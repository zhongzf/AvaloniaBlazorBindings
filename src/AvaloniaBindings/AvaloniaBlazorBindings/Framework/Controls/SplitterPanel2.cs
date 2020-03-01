using Microsoft.MobileBlazorBindings.Core;

namespace AvaloniaBlazorBindings.Framework.Controls
{
#pragma warning disable CA1812 // Internal class that is apparently never instantiated; this class is instantiated generically
    internal class SplitterPanel2 : SplitterPanelBase
#pragma warning restore CA1812 // Internal class that is apparently never instantiated
    {
        static SplitterPanel2()
        {
            ElementHandlerRegistry.RegisterElementHandler<SplitterPanel2>(
                (_, parentControl) => GetSplitterPanel(((IAvaloniaControlHandler)parentControl).Control, panelNumber: 2));
        }
    }
}
