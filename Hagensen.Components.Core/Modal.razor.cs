using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using System;

namespace Hagensen.Components.Core;

public partial class Modal
{
    [Parameter] public RenderFragment? Header { get; set; }
    [Parameter] public RenderFragment? Body { get; set; }
    [Parameter] public RenderFragment? Footer { get; set; }

    [Inject] public IJSRuntime? JSRuntime { get; set; }

    private string modalDisplay = "none";
    private string modalClass = "";
    private bool showBackdrop = false;

    public Modal()
    {
        moduleTask = new(() => (JSRuntime != null) ? JSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Hagensen.Components.Core/Modal.razor.js").AsTask() : throw new InvalidOperationException("Javascript runtime not injected"));
    }

    public void Open()
    {
        modalDisplay = "block";
        modalClass = "show";
        showBackdrop = true;

        InvokeAsync(async () =>
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("rememberActiveElement");
            var success = await module.InvokeAsync<bool>("focusFirstChildElement", BodyElement);
            if (!success)
                await module.InvokeAsync<bool>("focusFirstChildElement", DivElement);
        });

        InvokeAsync(() => StateHasChanged());
    }

    public void Close() => InvokeAsync(async () => await CloseAsync());

    public async Task CloseAsync()
    {
        modalDisplay = "none";
        modalClass = "";
        showBackdrop = false;

        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("restoreActiveElement");

        await InvokeAsync(() => StateHasChanged());
    }

    private void HandleModalKeyDown(ModalKeyboardEventArgs e)
    {
        if (e.Key == "Escape")
            Close();
    }

    protected override async Task OnInitializedAsync()
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("initialize", DivElement);
    }

    private ElementReference DivElement { get; set; }
    private ElementReference BodyElement { get; set; }

    private readonly Lazy<Task<IJSObjectReference>> moduleTask;
}
