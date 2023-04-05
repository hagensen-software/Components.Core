using Microsoft.AspNetCore.Components;
using System;

namespace Hagensen.Components.Core;

[EventHandler("onmodalkeydown", typeof(ModalKeyboardEventArgs))]
public static class EventHandlers
{
}

public class ModalKeyboardEventArgs : EventArgs
{
    public string? Key { get; set; }
}