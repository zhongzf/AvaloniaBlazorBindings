using Microsoft.MobileBlazorBindings.Core;

namespace AvaloniaBlazorBindings.Framework.Controls
{
#pragma warning disable CA1812 // Internal class that is apparently never instantiated; this class is instantiated generically
    internal class SplitterPanel1 : SplitterPanelBase
#pragma warning restore CA1812 // Internal class that is apparently never instantiated
    {
        static SplitterPanel1()
        {
            ElementHandlerRegistry.RegisterElementHandler<SplitterPanel1>(
                (_, parentControl) => GetSplitterPanel(((IAvaloniaControlHandler)parentControl).Control, panelNumber: 1));
        }
    }
}
