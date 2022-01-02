// <copyright file="App.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DrasticMaui.Sample;

using DrasticMaui.Events;

/// <summary>
/// App.
/// </summary>
public partial class App : Application
{
    private DrasticTrayIcon icon;

    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        this.InitializeComponent();
    }

    private void TrayIcon_MenuClicked(object? sender, DrasticTrayMenuClickedEventArgs e)
    {
        if (e.MenuItem.Text == "Exit")
        {
#if WINDOWS
            Microsoft.UI.Xaml.Application.Current.Exit();
            return;
#endif
        }
    }

    /// <inheritdoc/>
    protected override Window CreateWindow(IActivationState? activationState)
        => new DrasticMauiSampleWindow() { Page = new NavigationPage(new MainPage()) };
}
