using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaBlazorBindings.Framework
{
    public class AvaloniaDispatcher : Dispatcher
    {
        public override bool CheckAccess()
        {
            return Avalonia.Threading.Dispatcher.UIThread.CheckAccess();
        }

        public override Task InvokeAsync(Action workItem)
        {
            return Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(workItem);
        }

        public override Task InvokeAsync(Func<Task> workItem)
        {
            return Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(workItem);
        }

        public override Task<TResult> InvokeAsync<TResult>(Func<TResult> workItem)
        {
            return Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(workItem);
        }

        public override Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> workItem)
        {
            return Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(workItem);
        }
    }
}
