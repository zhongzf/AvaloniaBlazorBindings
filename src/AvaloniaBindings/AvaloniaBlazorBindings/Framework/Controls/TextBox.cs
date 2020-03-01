using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Styling;
using System;

namespace AvaloniaBlazorBindings.Framework.Controls
{
    public class TextBox : AvaloniaComponentBase
    {
        static TextBox()
        {
            ElementHandlerRegistry.RegisterElementHandler<TextBox>(renderer => new BlazorTextBox(renderer));
        }

        [Parameter] public string Text { get; set; }
        [Parameter] public bool? Multiline { get; set; }
        [Parameter] public bool? ReadOnly { get; set; }
        [Parameter] public bool? WordWrap { get; set; }
        //[Parameter] public ScrollBars? ScrollBars { get; set; }

        [Parameter] public EventCallback<string> TextChanged { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }
            if (Multiline != null)
            {
                builder.AddAttribute(nameof(Multiline), Multiline.Value);
            }
            if (ReadOnly != null)
            {
                builder.AddAttribute(nameof(ReadOnly), ReadOnly.Value);
            }
            if (WordWrap != null)
            {
                builder.AddAttribute(nameof(WordWrap), WordWrap.Value);
            }
            //if (ScrollBars != null)
            //{
            //    builder.AddAttribute(nameof(ScrollBars), (int)ScrollBars.Value);
            //}

            builder.AddAttribute("ontextchanged", EventCallback.Factory.Create<ChangeEventArgs>(this, HandleTextChanged));
        }

        private Task HandleTextChanged(ChangeEventArgs evt)
        {
            return TextChanged.InvokeAsync((string)evt.Value);
        }

        private class BlazorTextBox : Avalonia.Controls.TextBox, IStyleable, IAvaloniaControlHandler
        {
            Type IStyleable.StyleKey => typeof(Avalonia.Controls.TextBox);

            public BlazorTextBox(NativeComponentRenderer renderer)
            {
                Renderer = renderer;
            }

            protected override void OnTextInput(TextInputEventArgs e)
            {
                if (TextChangedEventHandlerId != default)
                {
                    Renderer.DispatchEventAsync(TextChangedEventHandlerId, null, new ChangeEventArgs { Value = Text });
                }
            }

            public ulong TextChangedEventHandlerId { get; set; }
            public NativeComponentRenderer Renderer { get; }

            public Control Control => this;
            public object TargetElement => this;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Text):
                        Text = (string)attributeValue;
                        break;
                    //case nameof(Multiline):
                    //    Multiline = AttributeHelper.GetBool(attributeValue);
                    //    break;
                    //case nameof(ReadOnly):
                    //    ReadOnly = AttributeHelper.GetBool(attributeValue);
                    //    break;
                    //case nameof(WordWrap):
                    //    WordWrap = AttributeHelper.GetBool(attributeValue);
                    //    break;
                    //case nameof(ScrollBars):
                    //    ScrollBars = (ScrollBars)AttributeHelper.GetInt(attributeValue);
                    //    break;
                    case "ontextchanged":
                        Renderer.RegisterEvent(attributeEventHandlerId, id => { if (TextChangedEventHandlerId == id) TextChangedEventHandlerId = 0; });
                        TextChangedEventHandlerId = attributeEventHandlerId;
                        break;
                    default:
                        AvaloniaComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
