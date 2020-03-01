using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Styling;
using System;

namespace AvaloniaBlazorBindings.Framework.Controls
{
    public class CheckBox : AvaloniaComponentBase
    {
        static CheckBox()
        {
            ElementHandlerRegistry.RegisterElementHandler<CheckBox>(renderer => new BlazorCheckBox(renderer));
        }

        [Parameter] public string Text { get; set; }
        [Parameter] public bool? Checked { get; set; }
        //[Parameter] public CheckState? CheckState { get; set; }
        [Parameter] public bool? ThreeState { get; set; }
        [Parameter] public EventCallback<bool> CheckedChanged { get; set; }
        //[Parameter] public EventCallback<CheckState> CheckStateChanged { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }
            if (Checked != null)
            {
                builder.AddAttribute(nameof(Checked), Checked.Value);
            }
            //if (CheckState != null)
            //{
            //    builder.AddAttribute(nameof(CheckState), (int)CheckState.Value);
            //}
            if (ThreeState != null)
            {
                builder.AddAttribute(nameof(ThreeState), ThreeState.Value);
            }

            builder.AddAttribute("oncheckedchanged", EventCallback.Factory.Create<ChangeEventArgs>(this, HandleCheckedChanged));
            builder.AddAttribute("oncheckstatechanged", EventCallback.Factory.Create<ChangeEventArgs>(this, HandleCheckStateChanged));
        }

        private Task HandleCheckedChanged(ChangeEventArgs evt)
        {
            return CheckedChanged.InvokeAsync((bool)evt.Value);
        }

        private Task HandleCheckStateChanged(ChangeEventArgs evt)
        {
            //return CheckStateChanged.InvokeAsync((CheckState)evt.Value);
            return Task.CompletedTask;
        }

        private class BlazorCheckBox : Avalonia.Controls.CheckBox, IStyleable, IAvaloniaControlHandler
        {
            Type IStyleable.StyleKey => typeof(Avalonia.Controls.CheckBox);

            public BlazorCheckBox(NativeComponentRenderer renderer)
            {
                Checked += (s, e) =>
                {
                    if (CheckedChangedEventHandlerId != default)
                    {
                        renderer.DispatchEventAsync(CheckedChangedEventHandlerId, null, new ChangeEventArgs { Value = IsChecked });
                    }
                };
                Unchecked += (s, e) =>
                {
                    if (CheckedChangedEventHandlerId != default)
                    {
                        renderer.DispatchEventAsync(CheckedChangedEventHandlerId, null, new ChangeEventArgs { Value = IsChecked });
                    }
                };
                Renderer = renderer;
            }

            public ulong CheckedChangedEventHandlerId { get; set; }
            public ulong CheckStateChangedEventHandlerId { get; set; }
            public NativeComponentRenderer Renderer { get; }

            public Control Control => this;
            public object TargetElement => this;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Text):
                        Content = (string)attributeValue;
                        break;
                    case nameof(Checked):
                        IsChecked = AttributeHelper.GetBool(attributeValue);
                        break;
                    case nameof(ThreeState):
                        IsThreeState = AttributeHelper.GetBool(attributeValue);
                        break;
                    case "oncheckedchanged":
                        Renderer.RegisterEvent(attributeEventHandlerId, id => { if (CheckedChangedEventHandlerId == id) CheckedChangedEventHandlerId = 0; });
                        CheckedChangedEventHandlerId = attributeEventHandlerId;
                        break;
                    case "oncheckstatechanged":
                        Renderer.RegisterEvent(attributeEventHandlerId, id => { if (CheckStateChangedEventHandlerId == id) CheckStateChangedEventHandlerId = 0; });
                        CheckStateChangedEventHandlerId = attributeEventHandlerId;
                        break;
                    default:
                        AvaloniaComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
