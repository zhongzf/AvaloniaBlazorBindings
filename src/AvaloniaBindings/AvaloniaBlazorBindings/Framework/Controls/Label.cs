using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Avalonia.Controls;
using Avalonia.Styling;
using System;

namespace AvaloniaBlazorBindings.Framework.Controls
{
    public class Label : AvaloniaComponentBase
    {
        static Label()
        {
            ElementHandlerRegistry.RegisterElementHandler<Label, BlazorLabel>();
        }

        [Parameter] public string Text { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }
        }

        private class BlazorLabel : Avalonia.Controls.TextBlock, IStyleable, IAvaloniaControlHandler
        {
            Type IStyleable.StyleKey => typeof(Avalonia.Controls.TextBlock);

            public Control Control => this;
            public object TargetElement => this;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Text):
                        Text = (string)attributeValue;
                        break;
                    default:
                        AvaloniaComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
