// <copyright file="App.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DrasticMaui.Sample;

using DrasticMaui.Events;
using DrasticMaui.Models;

/// <summary>
/// App.
/// </summary>
public partial class App : DrasticApp
{
    private SidebarMenuOptions sidebarMenuOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    /// <param name="provider">Service Provider.</param>
    public App(IServiceProvider provider)
        : base(provider)
    {
        this.InitializeComponent();
        var icon = MauiProgram.GetResourceFileContent("Icon.favicon.ico");
        var menuList = new List<NavigationSidebarItem>();
        menuList.Add(new NavigationSidebarItem("Main Page", imageStream: icon, page: new NavigationPage(new MainPage(provider))));
        menuList.Add(new NavigationSidebarItem("Test 1", imageStream: icon, page: new NavigationPage(new MauiTestPage())));
        menuList.Add(new NavigationSidebarItem("Test 2", imageStream: icon, page: new MauiTestPage()));
        this.sidebarMenuOptions = new SidebarMenuOptions("DrasticMaui", menuList, true);
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

    ///// <inheritdoc/>
    //protected override Window CreateWindow(IActivationState? activationState)
    //    => new DrasticMauiSampleWindow(this.Services) { Page = new NavigationPage(new MainPage(this.Services)) };

    /// <inheritdoc/>
    protected override Window CreateWindow(IActivationState? activationState)
        => new DrasticMauiSampleWindow(this.sidebarMenuOptions, this.Services);
}
