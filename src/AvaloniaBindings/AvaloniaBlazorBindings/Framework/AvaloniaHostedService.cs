using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Logging.Serilog;
using Avalonia.Threading;
using AvaloniaBlazorBindings.Framework.Controls;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaBlazorBindings.Framework
{
    /// <summary>
    /// An implementation of <see cref="IHostedService"/> that controls the lifetime of a BlinForms application.
    /// When this service starts, it loads the main form registered by
    /// <see cref="BlinFormsServiceCollectionExtensions.AddRootFormContent{TComponent}(Microsoft.Extensions.DependencyInjection.IServiceCollection)"/>.
    /// The service will request that the application stops when the main form is closed.
    /// This service will invoke all instances of <see cref="IAvaloniaStartup"/> that are registered in the
    /// container. The order of the startup instances is not guaranteed.
    /// </summary>
    public class AvaloniaHostedService : IHostedService, IDisposable
    {
        private readonly IAvaloniaRootFormContent _avaloniaRootFormContent;
        public IAvaloniaRootFormContent RootFormContent => _avaloniaRootFormContent;

        private readonly ILoggerFactory _loggerFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IEnumerable<IAvaloniaStartup> _blinFormsStartups;

        private AvaloniaRenderer _renderer;
        public AvaloniaRenderer Renderer => _renderer;

        public static AvaloniaHostedService Current { get; private set; }

        public AvaloniaHostedService(
            IAvaloniaRootFormContent avaloniaRootFormContent,
            ILoggerFactory loggerFactory,
            IServiceProvider serviceProvider,
            IHostApplicationLifetime hostApplicationLifetime,
            IEnumerable<IAvaloniaStartup> blinFormsStartups)
        {
            Current = this;

            _avaloniaRootFormContent = avaloniaRootFormContent;
            _loggerFactory = loggerFactory;
            _serviceProvider = serviceProvider;
            _hostApplicationLifetime = hostApplicationLifetime;
            _blinFormsStartups = blinFormsStartups;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (_blinFormsStartups != null)
            {
                await Task.WhenAll(_blinFormsStartups.Select(async startup => await startup.OnStartAsync().ConfigureAwait(false))).ConfigureAwait(false);
            }

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            _renderer = new AvaloniaRenderer(_serviceProvider, _loggerFactory);
            //await _renderer.Dispatcher.InvokeAsync(() =>
            //{
            //    //var rootForm = new RootForm();
            //    //rootForm.FormClosed += OnRootFormFormClosed;

            //    //await _renderer.AddComponent(_blinFormsMainForm.RootFormContentType, new ControlWrapper(rootForm)).ConfigureAwait(false);

            //    //Application.Run(rootForm);

            //}).ConfigureAwait(false);
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(Array.Empty<string>());
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToDebug();


        //private void OnRootFormFormClosed(object sender, FormClosedEventArgs e)
        //{
        //    // When the main form closes, request for the application to stop
        //    _hostApplicationLifetime.StopApplication();
        //}

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _renderer?.Dispose();
            _renderer = null;

            return Task.CompletedTask;
        }

        public sealed class ControlWrapper : IAvaloniaControlHandler
        {
            public ControlWrapper(Control control)
            {
                Control = control ?? throw new ArgumentNullException(nameof(control));
            }

            public Control Control { get; }
            public object TargetElement => Control;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                AvaloniaComponentBase.ApplyAttribute(Control, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _renderer?.Dispose();
            _renderer = null;
        }
    }
}
