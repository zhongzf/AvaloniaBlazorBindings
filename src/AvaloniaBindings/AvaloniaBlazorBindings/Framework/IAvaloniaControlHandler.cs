using Avalonia.Controls;
using Microsoft.MobileBlazorBindings.Core;

namespace AvaloniaBlazorBindings.Framework
{
    public interface IAvaloniaControlHandler : IElementHandler
    {
        Control Control { get; }
    }
}
