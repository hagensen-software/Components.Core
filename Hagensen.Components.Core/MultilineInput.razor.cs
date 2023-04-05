using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace Hagensen.Components.Core;

public partial class MultilineInput
{
    [Parameter] public string Text { get; set; } = string.Empty;
    [Parameter] public Color BackColor { get; set; } = Color.White;
    [Parameter] public string Class { get; set; } = string.Empty;
    [Parameter] public EventCallback<string> TextChanged { get; set; }

    [Inject] public IJSRuntime? JSRuntime { get; set; }

    public MultilineInput()
    {
        moduleTask = new(() => (JSRuntime != null) ? JSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Hagensen.Components.Core/MultilineInput.razor.js").AsTask() : throw new InvalidOperationException("Javascript runtime not injected"));
    }

    public Task FocusAsync()
    {
        takeFocus = true;

        return Task.CompletedTask;
    }

    public async Task OnBlur()
    {
        var module = await moduleTask.Value;
        var text = await module.InvokeAsync<string>("getInnerHTML", TextElement);
        await TextChanged.InvokeAsync(text);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("initialize", TextElement);
        }
        if (takeFocus)
        {
            takeFocus = false;

            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("select", TextElement);
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    private ElementReference TextElement { get; set; }

    private readonly Lazy<Task<IJSObjectReference>> moduleTask;
    private bool takeFocus = false;
}
