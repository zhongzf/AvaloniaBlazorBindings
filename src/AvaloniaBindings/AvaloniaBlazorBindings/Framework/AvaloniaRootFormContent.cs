using Microsoft.AspNetCore.Components;
using System;

namespace AvaloniaBlazorBindings.Framework
{
    public class AvaloniaRootFormContent<TComponent> : IAvaloniaRootFormContent
        where TComponent : IComponent
    {
        public AvaloniaRootFormContent()
        {
            RootFormContentType = typeof(TComponent);
        }

        public Type RootFormContentType { get; }
    }
}
