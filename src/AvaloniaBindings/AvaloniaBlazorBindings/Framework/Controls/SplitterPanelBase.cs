using Avalonia.Controls;
using Microsoft.AspNetCore.Components;

namespace AvaloniaBlazorBindings.Framework.Controls
{
    internal abstract class SplitterPanelBase : AvaloniaComponentBase
    {
        private protected static IAvaloniaControlHandler GetSplitterPanel(Control parentControl, int panelNumber)
        {
            if (!(parentControl is SplitContainer.BlazorSplitContainer splitContainer))
            {
                // This gets called from a static constructor, so we really don't want to throw from here
                return null;
            }
            return panelNumber switch
            {
                1 => new BlazorSplitterPanelWrapper(splitContainer.Panel1),
                2 => new BlazorSplitterPanelWrapper(splitContainer.Panel2),
                _ => null
            };
        }

        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override RenderFragment GetChildContent() => ChildContent;

        private sealed class BlazorSplitterPanelWrapper : IAvaloniaControlHandler
        {
            public BlazorSplitterPanelWrapper(Avalonia.Controls.Panel splitterPanel)
            {
                SplitterPanel = splitterPanel;
            }

            public Control Control => SplitterPanel;

            public object TargetElement => SplitterPanel;

            public Avalonia.Controls.Panel SplitterPanel { get; }

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                AvaloniaComponentBase.ApplyAttribute(SplitterPanel, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
            }
        }
    }
}
