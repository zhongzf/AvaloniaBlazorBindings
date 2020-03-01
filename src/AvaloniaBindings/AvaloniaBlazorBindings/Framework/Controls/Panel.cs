using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Avalonia.Controls;

namespace AvaloniaBlazorBindings.Framework.Controls
{
    public class Panel : AvaloniaComponentBase
    {
        static Panel()
        {
            ElementHandlerRegistry.RegisterElementHandler<Panel, BlazorPanel>();
        }

        [Parameter] public bool? AutoScroll { get; set; }
#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (AutoScroll != null)
            {
                builder.AddAttribute(nameof(AutoScroll), AutoScroll.Value);
            }
        }

        protected override RenderFragment GetChildContent() => ChildContent;

        private class BlazorPanel : Avalonia.Controls.StackPanel, IAvaloniaControlHandler
        {
            public Control Control => this;
            public object TargetElement => this;

            public BlazorPanel()
            {
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Left;
            }

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    //case nameof(AutoScroll):
                    //    AutoScroll = AttributeHelper.GetBool(attributeValue);
                    //    break;
                    default:
                        AvaloniaComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
