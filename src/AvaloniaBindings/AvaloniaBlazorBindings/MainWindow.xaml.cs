using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using AvaloniaBlazorBindings.Framework;
using AvaloniaBlazorBindings.Framework.Controls;
using static AvaloniaBlazorBindings.Framework.AvaloniaHostedService;

namespace AvaloniaBlazorBindings
{
    public class MainWindow : Window, IAvaloniaControlHandler
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        public Control Control => this;
        public object TargetElement => this;

        public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            AvaloniaComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            var _hostedService = AvaloniaHostedService.Current;
            var _renderer = _hostedService.Renderer;
            var _rootFormContent = _hostedService.RootFormContent;
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                await _renderer.AddComponent(_rootFormContent.RootFormContentType, new ControlWrapper(this)).ConfigureAwait(false);
            });
        }
    }
}
