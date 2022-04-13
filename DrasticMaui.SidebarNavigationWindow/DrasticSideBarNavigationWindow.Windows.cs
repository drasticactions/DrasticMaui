// <copyright file="DrasticSideBarNavigationWindow.Windows.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DrasticMaui.Models;
using Microsoft.Maui.Platform;

namespace DrasticMaui
{
    public partial class DrasticSideBarNavigationWindow
    {
        private Microsoft.UI.Xaml.FrameworkElement? page;
        private MauiNavigationView? navigationView;
        private WindowRootView? navigationRootView;
        private Page? selectedPage;
        private IMauiContext? context;

        private Microsoft.Maui.Graphics.Rectangle backButton = new Microsoft.Maui.Graphics.Rectangle(0, 0, 100, 100);

        public DrasticSideBarNavigationWindow(SidebarMenuOptions options, IServiceProvider services)
            : base(services)
        {
            this.Page = new ContentPage();
            this.options = options;
        }

        /// <summary>
        /// Setup Navigation View.
        /// </summary>
        public async void SetupNavigationView()
        {
            if (this.Page == null)
            {
                return;
            }

            var handler = this.Handler as Microsoft.Maui.Handlers.WindowHandler;

            if (handler?.MauiContext is null)
            {
                return;
            }

            this.context = handler.MauiContext;

            if (handler?.PlatformView is not Microsoft.UI.Xaml.Window window)
            {
                return;
            }

            if (window.Content is not Microsoft.UI.Xaml.Controls.Panel panel)
            {
                return;
            }

            if (panel.Children[0] is not WindowRootView rootView)
            {
                return;
            }

            this.navigationRootView = rootView;
            this.navigationRootView.Loaded += this.NavigationRootView_Loaded;
        }

        private void NavigationRootView_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (sender is not WindowRootView view)
            {
                return;
            }

            this.navigationView = view.NavigationViewControl;
            if (this.navigationView is null)
            {
                return;
            }

            this.navigationView.IsSettingsVisible = this.options.ShowSettingsItem;
            this.navigationView.SelectionChanged += this.NavigationView_SelectionChanged;
            var items = this.options.DefaultSidebarItems.Select(n => n.ToNavigationViewItem()).ToList();
            foreach (var item in items)
            {
                this.navigationView.MenuItems.Add(item);
            }

            this.navigationView.IsBackEnabled = true;
            this.navigationView.SelectedItem = this.navigationView.MenuItems.FirstOrDefault();
            this.navigationView.BackRequested += NavigationView_BackRequested;
            this.navigationView.PaneDisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.LeftCompact;
            this.navigationView.IsPaneToggleButtonVisible = true;

            this.isInitialized = true;
        }

        private void NavigationView_BackRequested(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs args)
        {
            if (this.selectedPage is not null && this.context is not null)
            {
                this.selectedPage.SendBackButtonPressed();
            }
        }

        private void NavigationView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {

            }
            else if (args.SelectedItem is Microsoft.UI.Xaml.Controls.NavigationViewItem item)
            {
                if (item.Tag is not Guid guid)
                {
                    return;
                }

                var selectedItem = this.options.DefaultSidebarItems.FirstOrDefault(n => n.Id == guid);
                if (selectedItem is null)
                {
                    return;
                }

                if (this.navigationRootView?.NavigationViewControl is null)
                {
                    return;
                }

                if (selectedItem.Page is null)
                {
                    return;
                }

                this.selectedPage = selectedItem.Page;

                this.selectedPage.Parent = this;

                if (this.Handler.MauiContext is null)
                {
                    return;
                }

                var testing = selectedItem.Page.ToHandler(this.Handler.MauiContext);
                var page = testing.PlatformView;

                bool addNavigationHandlers = false;
                if (page != this.page)
                {
                    addNavigationHandlers = true;
                    this.RemoveNavigationHandlers(sender, this.page);
                }

                this.navigationRootView.NavigationViewControl.Content = this.page = page;
                if (addNavigationHandlers)
                {
                    this.AddNavigationHandlers(sender, this.page);
                }
            }
        }

        private void RemoveNavigationHandlers(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.FrameworkElement? element)
        {
            if (element is not Microsoft.UI.Xaml.Controls.Frame frame)
            {
                return;
            }

            frame.Navigated -= Frame_Navigated;
        }

        private void AddNavigationHandlers(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.FrameworkElement? element)
        {
            if (element is not Microsoft.UI.Xaml.Controls.Frame frame)
            {
                sender.IsBackButtonVisible = Microsoft.UI.Xaml.Controls.NavigationViewBackButtonVisible.Collapsed;
                return;
            }

            frame.Navigated += Frame_Navigated;
            sender.IsBackButtonVisible = frame.BackStackDepth >= 1 ? Microsoft.UI.Xaml.Controls.NavigationViewBackButtonVisible.Visible : Microsoft.UI.Xaml.Controls.NavigationViewBackButtonVisible.Collapsed;
        }

        private void Frame_Navigated(object sender, Microsoft.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            if (sender is Microsoft.UI.Xaml.Controls.Frame frame && this.navigationView is not null)
            {
                this.navigationView.IsBackButtonVisible = frame.BackStackDepth >= 1 ? Microsoft.UI.Xaml.Controls.NavigationViewBackButtonVisible.Visible : Microsoft.UI.Xaml.Controls.NavigationViewBackButtonVisible.Collapsed;
            }
        }
    }

    public static class Extensions
    {
        public static Microsoft.UI.Xaml.Controls.NavigationViewItem ToNavigationViewItem(this NavigationSidebarItem item)
        {
            if (item.Image is not null)
            {
                var imageIcon = new Microsoft.UI.Xaml.Controls.ImageIcon() { Source = item.Image };
                return new Microsoft.UI.Xaml.Controls.NavigationViewItem() { Tag = item.Id, Content = item.Title, Icon = imageIcon };
            }

            return new Microsoft.UI.Xaml.Controls.NavigationViewItem() { Tag = item.Id, Content = item.Title };
        }
    }
}
