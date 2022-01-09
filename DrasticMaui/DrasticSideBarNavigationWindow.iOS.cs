// <copyright file="DrasticSideBarNavigationWindow.iOS.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DrasticMaui.iOS;
using DrasticMaui.Models;
using Microsoft.Maui.Platform;
using UIKit;

namespace DrasticMaui
{
    public partial class DrasticSideBarNavigationWindow
    {
        private MacSidebarMenuOptions macOptions;
        private UISplitViewController? splitView;
        private SidebarViewController? sidebarView;

        public DrasticSideBarNavigationWindow(
            Page content,
            SidebarMenuOptions options,
            IServiceProvider services,
            MacSidebarMenuOptions? macOptions = null)
            : base(services)
        {
            this.Page = content;
            this.options = options;
            this.macOptions = macOptions ?? new MacSidebarMenuOptions();
        }

        /// <summary>
        /// Setup Navigation View.
        /// </summary>
        public void SetupNavigationView()
        {
            if (this.Page == null)
            {
                return;
            }

            var window = this.Handler?.NativeView as UIWindow;
            if (window is null)
            {
                return;
            }

            var context = this.Handler?.MauiContext;
            if (context is null)
            {
                return;
            }

            // Create a split view controller.
            // Take the existing content and put it into the main panel.
            // Put the side panel content into the pane.
            this.splitView = new UISplitViewController();
            this.splitView.PrimaryBackgroundStyle = UISplitViewControllerBackgroundStyle.Sidebar;
            this.sidebarView = new SidebarViewController(this.options, this.macOptions);
            var b = this.Page.ToUIViewController(context);
            this.splitView.ViewControllers = new UIViewController[] { new UINavigationController(this.sidebarView), b };
            window.RootViewController = this.splitView;
        }
    }
}
