// <copyright file="DrasticSideBarNavigationWindow.Windows.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DrasticMaui.Models;
using DrasticMaui.Tools;
using Microsoft.Maui.Platform;

namespace DrasticMaui
{
    public partial class DrasticSideBarNavigationWindow
    {
        private Microsoft.UI.Xaml.Controls.NavigationView? navigationView;
        private NavigationRootView? navigationRootView;

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

            if (handler?.NativeView is not Microsoft.UI.Xaml.Window window)
            {
                return;
            }

            if (window.Content is not Microsoft.UI.Xaml.Controls.Panel panel)
            {
                return;
            }

            if (panel.Children[0] is not NavigationRootView rootView)
            {
                return;
            }

            this.navigationRootView = rootView;
            this.navigationRootView.Loaded += this.NavigationRootView_Loaded;
        }

        private void NavigationRootView_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (sender is not NavigationRootView view)
            {
                return;
            }

            this.navigationView = view.NavigationViewControl;
            if (this.navigationView is null)
            {
                return;
            }

            //this.navigationView = new Microsoft.UI.Xaml.Controls.NavigationView();
            //var testing = this.page.ToHandler(handler.MauiContext).GetWrappedNativeView();
            //this.navigationView.Content = testing;
            ////panel.PointerMoved += NavigationView_PointerMoved;
            this.navigationView.IsSettingsVisible = this.options.ShowSettingsItem;
            this.navigationView.SelectionChanged += this.NavigationView_SelectionChanged;
            var items = this.options.DefaultSidebarItems.Select(n => n.ToNavigationViewItem()).ToList();
            foreach (var item in items)
            {
                this.navigationView.MenuItems.Add(item);
            }

            //var nav = this.options.DefaultSidebarItems.FirstOrDefault();
            //if (nav?.Page is not null && this.Handler.MauiContext is not null)
            //{
            //    var navItem = items.First();
            //    this.navigationView.SelectedItem = navItem;
            //    view.Content = nav.Page.ToHandler(this.Handler.MauiContext).GetWrappedNativeView();
            //}

            this.navigationView.PaneDisplayMode = Microsoft.UI.Xaml.Controls.NavigationViewPaneDisplayMode.Auto;
            //window.ExtendsContentIntoTitleBar = false;
            //panel.Children.Add(this.navigationView);
            this.isInitialized = true;
        }

        private void NavigationView_PointerMoved(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (this.navigationView is null)
            {
                return;
            }

            var pointerPoint = e.GetCurrentPoint(this.navigationView);
            if (pointerPoint == null)
            {
                return;
            }

            if (this.navigationView.Content is Microsoft.UI.Xaml.FrameworkElement element)
            {
                this.navigationView.IsHitTestVisible = !element.GetBoundingBox().Contains(new Microsoft.Maui.Graphics.Point(pointerPoint.Position.X, pointerPoint.Position.Y));
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

                if (this.Handler.MauiContext is null)
                {
                    return;
                }

                this.navigationRootView.NavigationViewControl.Content = selectedItem.Page.ToHandler(this.Handler.MauiContext).GetWrappedNativeView();
            }
        }
    }
}
