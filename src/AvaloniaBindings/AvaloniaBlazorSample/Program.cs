﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using AvaloniaBlazorBindings.Framework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace AvaloniaBlazorSample
{
    internal static class Program
    {
        [STAThread]
        private static async Task Main()
        {
            await Host.CreateDefaultBuilder()
                .AddAvaloniaBindings()
                .ConfigureServices((hostContext, services) =>
                {
                    // Register app-specific services
                    services.AddSingleton<AppState>();
                    services.AddSingleton<IAvaloniaStartup, TodoAppStartup>();

                    // Register root form content
                    services.AddRootFormContent<TodoApp>();
                })
                .Build()
                .RunAsync();
        }
    }
}
