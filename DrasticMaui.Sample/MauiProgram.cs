// <copyright file="MauiProgram.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DrasticMaui.Tray;
using System.Reflection;

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
        var stream = GetResourceFileContent("Icon.favicon.ico");
        if (stream is not null)
        {
            var tray = new TrayService("DrasticMaui", stream);
            tray.SetupTrayIcon();
        }

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        return builder.Build();
    }

    private static Stream? GetResourceFileContent(string fileName)
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
