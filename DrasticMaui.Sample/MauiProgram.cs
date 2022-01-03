// <copyright file="MauiProgram.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Reflection;
using DrasticMaui.Logger;
using DrasticMaui.Sample.ViewModels;
using DrasticMaui.Services;

namespace DrasticMaui.Sample;

/// <summary>
/// Maui Program.
/// </summary>
public static class MauiProgram
{
    /// <summary>
    /// Create Maui App.
    /// </summary>
    /// <returns>MauiApp.</returns>
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.Services.AddSingleton<ILogger, ConsoleLogger>();
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IErrorHandlerService, ErrorHandlerService>();
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<PageOverlaySample>();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        return builder.Build();
    }

    /// <summary>
    /// Get Resource File Content via FileName.
    /// </summary>
    /// <param name="fileName">Filename.</param>
    /// <returns>Stream.</returns>
    public static Stream? GetResourceFileContent(string fileName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "DrasticMaui.Sample." + fileName;
        if (assembly is null)
        {
            return null;
        }

        return assembly.GetManifestResourceStream(resourceName);
    }
}
