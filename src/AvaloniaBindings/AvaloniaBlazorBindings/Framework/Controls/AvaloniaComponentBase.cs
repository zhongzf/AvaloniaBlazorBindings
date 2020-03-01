using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using System;
using System.Drawing;
using Avalonia.Controls;
using Avalonia;

namespace AvaloniaBlazorBindings.Framework.Controls
{
    public abstract class AvaloniaComponentBase : NativeControlComponentBase
    {
        [Parameter] public int? Top { get; set; }
        [Parameter] public int? Left { get; set; }
        [Parameter] public int? Width { get; set; }
        [Parameter] public int? Height { get; set; }

        [Parameter] public int? TabIndex { get; set; }

        [Parameter] public bool? Visible { get; set; }
        [Parameter] public Color? BackColor { get; set; }
        [Parameter] public Font Font { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            if (Top != null)
            {
                builder.AddAttribute(nameof(Top), Top.Value);
            }
            if (Left != null)
            {
                builder.AddAttribute(nameof(Left), Left.Value);
            }
            if (Width != null)
            {
                builder.AddAttribute(nameof(Width), Width.Value);
            }
            if (Height != null)
            {
                builder.AddAttribute(nameof(Height), Height.Value);
            }
            if (Visible != null)
            {
                builder.AddAttribute(nameof(Visible), Visible.Value);
            }

            if (BackColor != null)
            {
                builder.AddAttribute(nameof(BackColor), BackColor.Value.ToArgb());
            }
        }

#pragma warning disable IDE0060 // Remove unused parameter; will likely be used in the future
#pragma warning disable CA1801 // Parameter is never used; will likely be used in the future
        public static void ApplyAttribute(Control control, ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
#pragma warning restore CA1801 // Parameter is never used
#pragma warning restore IDE0060 // Remove unused parameter
        {
            if (control is null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            switch (attributeName)
            {
                case nameof(Top):
                    control.Arrange(new Rect(control.Bounds.X, AttributeHelper.GetInt(attributeValue), control.Bounds.Width, control.Bounds.Height));
                    break;
                case nameof(Left):
                    control.Arrange(new Rect(AttributeHelper.GetInt(attributeValue), control.Bounds.Y, control.Bounds.Width, control.Bounds.Height));
                    break;
                case nameof(Width):
                    control.Width = AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(Height):
                    control.Height = AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(Visible):
                    control.IsVisible = AttributeHelper.GetBool(attributeValue);
                    break;

                case nameof(BackColor):
                    {
                        var color = Avalonia.Media.Color.FromUInt32((uint)AttributeHelper.GetInt(attributeValue));
                        if (control is Avalonia.Controls.Button button)
                        {
                            button.Background = new Avalonia.Media.SolidColorBrush(color);
                        }
                        else if (control is Avalonia.Controls.Panel panel)
                        {
                            panel.Background = new Avalonia.Media.SolidColorBrush(color);
                        }
                        break;
                    }
                    //default:
                    //    throw new NotImplementedException($"FormsComponentBase doesn't recognize attribute '{attributeName}'");
            }
        }
    }
}
