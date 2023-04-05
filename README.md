# Hagensen.Components.Core

This project contains a set of minimal or core Blazor component for use in a WebAssembly project.

Build the solution using Visual Studio 2022 or newer and add the NuGet package generated to you Blazor project.
The components are used as described below.

## Modal & ModalDialogBase

Implementation of a modal dialog that when activated grays out the background and shows a modal dialog with a header, body and footer.
The dialog automatically select the first focusable element in the body section and prevents unintentionally tabbing outside the dialog.
It also remembers the previously selected element and sets focus back to that element if it still can receive focus.
Otherwise it tries to find the nearest parent that accepts focus.

Create a razor component to define the content of your modal dialog.
The content of the dialog should follow the following template (assumes bootstrap classes):

File: MyModalDialog.razor.cs:

```razor
@using Hagensen.Components.Core

@inherits ModalDialogBase

<Modal @ref="modal">
    <Header>
        // Insert your title and othe header content here
        <button type="button" class="btn-close" data-dismiss="modal" aria-label="Close" onclick="@(() => Close())"></button>
    </Header>
    <Body>
        // Insert your boby content here
    </Body>
    <Footer>
        <button class="btn btn-secondary" onclick="@(() => Close())">Close</button>
        <button class="btn btn-primary" onclick="@(() => Ok())">Ok</button> // ... or other actions to perform
    </Footer>
</Modal>
```

Note, that the component inherits ModalDialogBase and the code-behind class must also do that if you have one.

I your code section or code-behind implement the following methods:

```csharp
/// <summary>
/// Open and initialize the dialog
/// </summary>
public void Open(/* Insert your input parameters to the dialog here */)
{
    // Assign local data and fields here

    Open();
}

/// <summary>
/// Handle Ok clicked.
/// If you need async behavior return Task and use CloseAsync() instead of Close()
/// </summary>
private void Ok()
{
    // Perform the required actions here

    return Close();
}
```

On your page or component that will use the modal dialog insert the component like this.

```razor
<MyModalDialog @ref=myModalDialog></MyModalDialog>
```

From the code section or code-behind activate the modal dialog like this.

```csharp
private MyModalDialog? myModalDialog;

myModalDialog?.Open(/* insert parameters here */);
```

## MultilineInput

Implementation of a multiline input field that automatically resize to fit the text entered into the field.

Use the component in place of an ordinary input tag to get a multiline text field.

```razor
<MultilineInput Class="form-control" @bind-Text="MyText"/>
```

Binding works as usual keeping the text field and the member in sync.
