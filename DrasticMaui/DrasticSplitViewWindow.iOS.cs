// <copyright file="DrasticSplitViewWindow.iOS.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using DrasticMaui.Tools;
using Microsoft.Maui.Platform;
using UIKit;

namespace DrasticMaui
{
    /// <summary>
    /// Drastic Split View Window.
    /// </summary>
    public partial class DrasticSplitViewWindow
    {
        private UISplitViewController? splitView;

        public void SetupSplitView()
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

            this.splitView = new UISplitViewController();
            this.splitView.PrimaryBackgroundStyle = UISplitViewControllerBackgroundStyle.Sidebar;
            var a = this.menu.ToUIViewController(context);
            var b = this.Page.ToUIViewController(context);
            this.splitView.ViewControllers = new UIViewController[] { a, b };

            window.RootViewController = this.splitView;
        }
    }
}