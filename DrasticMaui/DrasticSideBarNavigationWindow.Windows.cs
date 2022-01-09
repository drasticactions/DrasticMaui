// <copyright file="DrasticSideBarNavigationWindow.Windows.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DrasticMaui.Models;
using DrasticMaui.Tools;

namespace DrasticMaui
{
    public partial class DrasticSideBarNavigationWindow
    {
        private Microsoft.UI.Xaml.Controls.NavigationView? navigationView;
        private Microsoft.UI.Xaml.Shapes.Rectangle tapGrid = new Microsoft.UI.Xaml.Shapes.Rectangle() { Fill = new Microsoft.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0)) };

        public DrasticSideBarNavigationWindow(Page content, SidebarMenuOptions options, IServiceProvider services)
            : base(services)
        {
            this.Page = content;
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
            if (handler?.NativeView is not Microsoft.UI.Xaml.Window window)
            {
                return;
            }

            if (window.Content is not Microsoft.UI.Xaml.Controls.Panel panel)
            {
                return;
            }

            this.navigationView = new Microsoft.UI.Xaml.Controls.NavigationView();
            this.navigationView.Content = this.tapGrid;
            panel.PointerMoved += NavigationView_PointerMoved;
            this.navigationView.IsSettingsVisible = this.options.ShowSettingsItem;
            this.navigationView.SelectionChanged += this.NavigationView_SelectionChanged;
            var items = this.options.DefaultSidebarItems.Select(n => n.ToNavigationViewItem()).ToList();
            foreach (var item in items)
            {
                this.navigationView.MenuItems.Add(item);
            }

            //var existingChildren = panel.Children.ToList();
            //panel.Children.Clear();

            panel.Children.Add(this.navigationView);

            //foreach (var child in existingChildren)
            //{
            //    panel.Children.Add(child);
            //}

            window.ExtendsContentIntoTitleBar = false;
            this.navigationView.IsPaneOpen = true;
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

            this.navigationView.IsHitTestVisible = !this.tapGrid.GetBoundingBox().Contains(new Microsoft.Maui.Graphics.Point(pointerPoint.Position.X, pointerPoint.Position.Y));
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

                // TODO: Handle Navigation.
            }
        }
    }
}
