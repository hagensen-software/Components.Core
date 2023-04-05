using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Hagensen.Components.Core;

public class ModalDialogBase : ComponentBase
{
    protected Modal? modal;

    public virtual void Open()
    {
        modal?.Open();

        InvokeAsync(() => StateHasChanged());
    }

    public virtual void Close()
    {
        modal?.Close();
    }

    public virtual Task CloseAsync()
    {
        if (modal == null)
            return Task.CompletedTask;

        return modal.CloseAsync();
    }
}
