// <copyright file="MainPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DrasticMaui.Sample.ViewModels;
using DrasticMaui.Tools;

namespace DrasticMaui.Sample;

/// <summary>
/// Main Page.
/// </summary>
public partial class MainPage : BasePage
{
    private bool isFullScreen;
    private DrasticMauiSampleWindow? window;
    private PageOverlaySample? sample;
    private MainPageViewModel vm;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainPage"/> class.
    /// </summary>
    /// <param name="services">IServiceProvider.</param>
    public MainPage(IServiceProvider services)
        : base(services)
    {
        this.InitializeComponent();
        this.BindingContext = this.ViewModel = services.ResolveWith<MainPageViewModel>(this);
    }

    /// <inheritdoc/>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        this.window = this.GetParentWindow() as DrasticMauiSampleWindow;
        if (this.window is null)
        {
            return;
        }
    }

    private async void OnPageNavigate(object sender, EventArgs e)
    {
        await this.Navigation.PushAsync(new DrasticMainMenu());
    }

    private void OnFullScreen(object sender, EventArgs e)
    {
        this.isFullScreen = !isFullScreen;
        this.window?.ToggleFullScreen(isFullScreen);
    }

    private void OnTrayIcon(object sender, EventArgs e)
    {
        this.window?.SetupTrayIcon();
    }

    private void OnNewTrayApp(object sender, EventArgs e)
    {
        this.window?.EnableTrayApp();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
    }
}