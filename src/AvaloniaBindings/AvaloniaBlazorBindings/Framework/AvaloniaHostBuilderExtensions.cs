using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AvaloniaBlazorBindings.Framework
{
    public static class AvaloniaHostBuilderExtensions
    {
        /// <summary>
        /// Registers <see cref="AvaloniaHostedService"/> in the DI container. Call this as part of configuring the
        /// host to enable BlinForms.
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <returns></returns>
        public static IHostBuilder AddAvaloniaBindings(this IHostBuilder hostBuilder)
        {
            if (hostBuilder is null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            hostBuilder.ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<AvaloniaHostedService>();
            });

            return hostBuilder;
        }
    }
}
