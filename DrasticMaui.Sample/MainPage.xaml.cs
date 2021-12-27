// <copyright file="MainPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DrasticMaui.Sample;

/// <summary>
/// Main Page.
/// </summary>
public partial class MainPage : ContentPage
{
    private DrasticMauiSampleWindow? window;
    private PageOverlaySample? sample;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainPage"/> class.
    /// </summary>
    public MainPage()
    {
        this.InitializeComponent();
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

    private void OnPageOverlay(object sender, EventArgs e)
    {
        if (this.window is null)
        {
            return;
        }

        this.sample ??= new PageOverlaySample(this.window);
        this.window?.PageOverlay.AddView(this.sample);
    }

    private void OnNewWindow(object sender, EventArgs e)
    {
        var newWindow = new DrasticSplitViewWindow(new TraySample(), new MainPage(), this.Handler.MauiContext);
        Application.Current?.OpenWindow(newWindow);
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
    }
}