// <copyright file="MainPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DrasticMaui.Tools;

namespace DrasticMaui.WindowSample;

/// <summary>
/// Main Page.
/// </summary>
public partial class MainPage : ContentPage
{
    private DrasticWindow? parentWindow;

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

        if (this.GetParentWindow() is DrasticWindow win)
        {
            this.parentWindow = win;
        }
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
    }

    private void FullScreenButton_Clicked(object sender, EventArgs e)
    {
        if (this.parentWindow is null)
        {
            return;
        }

#if MACCATALYST || WINDOWS10_0_19041_0_OR_GREATER
        this.parentWindow.ToggleFullScreen(!this.parentWindow.IsFullscreen());
#else
        // Throw error message.
#endif
    }
}