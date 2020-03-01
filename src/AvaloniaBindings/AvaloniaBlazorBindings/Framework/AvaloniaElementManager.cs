using Microsoft.MobileBlazorBindings.Core;
using System.Diagnostics;

namespace AvaloniaBlazorBindings.Framework
{
    internal class AvaloniaElementManager : ElementManager<IAvaloniaControlHandler>
    {
        protected override void RemoveElement(IAvaloniaControlHandler handler)
        {
            if (handler.Control.Parent is Avalonia.Controls.IPanel panel)
            {
                panel.Children.Remove(handler.Control);
            }
        }

        protected override void AddChildElement(IAvaloniaControlHandler parentHandler, IAvaloniaControlHandler childHandler, int physicalSiblingIndex)
        {
            if (parentHandler.Control is Avalonia.Controls.IPanel panel)
            {
                if (physicalSiblingIndex <= panel.Children.Count)
                {
                    panel.Children.Insert(physicalSiblingIndex, childHandler.Control);
                }
                else
                {
                    //Debug.WriteLine($"WARNING: {nameof(AddChildElement)} called with {nameof(physicalSiblingIndex)}={physicalSiblingIndex}, but parentControl.Controls.Count={parentHandler.Control.Controls.Count}");
                    panel.Children.Add(childHandler.Control);
                }
            }
            else if(parentHandler.Control is Avalonia.Controls.IContentControl contentControl)
            {
                contentControl.Content = childHandler.Control;
            }
        }

        protected override int GetPhysicalSiblingIndex(IAvaloniaControlHandler handler)
        {
            return (handler.Control.Parent as Avalonia.Controls.IPanel).Children.IndexOf(handler.Control);
        }

        protected override bool IsParented(IAvaloniaControlHandler handler)
        {
            return handler.Control.Parent != null;
        }

        protected override bool IsParentOfChild(IAvaloniaControlHandler parentHandler, IAvaloniaControlHandler childHandler)
        {
            return (parentHandler.Control as Avalonia.Controls.IPanel).Children.Contains(childHandler.Control);
        }
    }
}
