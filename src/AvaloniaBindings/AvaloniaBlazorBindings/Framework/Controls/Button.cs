using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using System;
using Avalonia.Controls;
using Avalonia.Styling;

namespace AvaloniaBlazorBindings.Framework.Controls
{
    public class Button : AvaloniaComponentBase
    {
        static Button()
        {
            ElementHandlerRegistry.RegisterElementHandler<Button>(
                renderer => new BlazorButton(renderer));
        }

        [Parameter] public string Text { get; set; }
        [Parameter] public EventCallback OnClick { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }

            builder.AddAttribute("onclick", OnClick);
        }

        private class BlazorButton : Avalonia.Controls.Button, IStyleable, IAvaloniaControlHandler
        {
            Type IStyleable.StyleKey => typeof(Avalonia.Controls.Button);

            public BlazorButton(NativeComponentRenderer renderer)
            {
                Width = 120;
                Height = 40;
                Click += (s, e) =>
                {
                    if (ClickEventHandlerId != default)
                    {
                        renderer.DispatchEventAsync(ClickEventHandlerId, null, new EventArgs());
                    }
                };
                Renderer = renderer;
            }

            public ulong ClickEventHandlerId { get; set; }
            public NativeComponentRenderer Renderer { get; }

            public Control Control => this;
            public object TargetElement => this;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Text):
                        this.Content = (string)attributeValue;
                        break;
                    case "onclick":
                        Renderer.RegisterEvent(attributeEventHandlerId, id => { if (ClickEventHandlerId == id) ClickEventHandlerId = 0; });
                        ClickEventHandlerId = attributeEventHandlerId;
                        break;
                    default:
                        AvaloniaComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
