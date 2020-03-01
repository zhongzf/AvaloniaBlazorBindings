using Microsoft.MobileBlazorBindings.Core;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace AvaloniaBlazorBindings.Framework
{
    public class AvaloniaRenderer : NativeComponentRenderer
    {
        public AvaloniaRenderer(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
            : base(serviceProvider, loggerFactory)
        {
        }

        protected override void HandleException(Exception exception)
        {
            //MessageBox.Show(exception?.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Debug.WriteLine(exception?.Message);
        }

        protected override ElementManager CreateNativeControlManager()
        {
            return new AvaloniaElementManager();
        }

        public override Dispatcher Dispatcher => new AvaloniaDispatcher();
    }
}
