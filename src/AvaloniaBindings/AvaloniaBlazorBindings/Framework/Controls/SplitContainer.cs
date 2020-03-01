using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Avalonia.Controls;
using Avalonia.Layout;

namespace AvaloniaBlazorBindings.Framework.Controls
{
    public class SplitContainer : AvaloniaComponentBase
    {
        static SplitContainer()
        {
            ElementHandlerRegistry.RegisterElementHandler<SplitContainer, BlazorSplitContainer>();
        }

        [Parameter] public RenderFragment Panel1 { get; set; }
        [Parameter] public RenderFragment Panel2 { get; set; }

        [Parameter] public Orientation? Orientation { get; set; }
        [Parameter] public int? SplitterDistance { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Orientation != null)
            {
                builder.AddAttribute(nameof(Orientation), (int)Orientation.Value);
            }
            if (SplitterDistance != null)
            {
                builder.AddAttribute(nameof(SplitterDistance), SplitterDistance.Value);
            }
        }

        protected override RenderFragment GetChildContent() => RenderChildContent;

        private void RenderChildContent(RenderTreeBuilder builder)
        {
            builder.OpenComponent<SplitterPanel1>(0);
            builder.AddAttribute(1, nameof(SplitterPanel1.ChildContent), Panel1);
            builder.CloseComponent();

            builder.OpenComponent<SplitterPanel2>(2);
            builder.AddAttribute(3, nameof(SplitterPanel2.ChildContent), Panel2);
            builder.CloseComponent();
        }

        public class BlazorSplitContainer : Avalonia.Controls.StackPanel, IAvaloniaControlHandler
        {
            public Control Control => this;
            public object TargetElement => this;

            public Avalonia.Controls.Panel Panel1 { get; set; }
            public Avalonia.Controls.Panel Panel2 { get; set; }

            public BlazorSplitContainer()
            {
                Orientation = Orientation.Horizontal;
                Panel1 = new Avalonia.Controls.Panel();
                Panel1.Width = 350;
                Children.Add(Panel1);
                Panel2 = new Avalonia.Controls.Panel();
                Panel2.Width = 300;
                Children.Add(Panel2);
            }

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Orientation):
                        Orientation = (Orientation)AttributeHelper.GetInt(attributeValue);
                        break;
                    //case nameof(SplitterDistance):
                    //    SplitterDistance = AttributeHelper.GetInt(attributeValue);
                    //    break;
                    default:
                        AvaloniaComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
