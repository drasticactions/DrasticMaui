// <copyright file="App.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DrasticMaui.Sample;

using DrasticMaui.Tray;

/// <summary>
/// App.
/// </summary>
public partial class App : Application
{
    private TrayService tray;

    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        this.InitializeComponent();
        this.HandlerChanged += App_HandlerChanged;
    }

    private void App_HandlerChanged(object? sender, EventArgs e)
    {
        var handler = this.Handler.MauiContext;
        var stream = MauiProgram.GetResourceFileContent("Icon.favicon.ico");
        if (stream is not null && handler is not null)
        {
            this.tray = new TrayService("DrasticMaui", stream, handler);
            this.tray.SetupPage(new TraySample());
            this.Dispatcher.Dispatch(this.tray.SetupTrayIcon);
        }
    }

    /// <inheritdoc/>
    protected override void OnStart()
    {
        base.OnStart();
    }

    /// <inheritdoc/>
    protected override Window CreateWindow(IActivationState? activationState)
        => new DrasticMauiSampleWindow() { Page = new MainPage() };
}
