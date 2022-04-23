// <copyright file="App.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DrasticMaui.WindowSample;

/// <summary>
/// Application.
/// </summary>
public partial class App : Application
{
    private IServiceProvider serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    /// <param name="services"><see cref="IServiceProvider"/>.</param>
    public App(IServiceProvider services)
    {
        this.serviceProvider = services;
        this.InitializeComponent();
    }

    /// <inheritdoc/>
    protected override Window CreateWindow(IActivationState? activationState)
        => new DrasticWindow(this.serviceProvider) { Page = new MainPage() };
}
