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

            this.splitView = new UISplitViewController();
            this.splitView.PrimaryBackgroundStyle = UISplitViewControllerBackgroundStyle.Sidebar;
            var a = this.menu.ToUIViewController(this.context);
            if (a.View is not null)
            {
                a.View.BackgroundColor = UIColor.Clear;
            }

            var b = this.Page.ToUIViewController(this.context);
            this.splitView.ViewControllers = new UIViewController[] { a, b };
            var window = this.Handler?.NativeView as UIWindow;
            if (window is null)
            {
                return;
            }

            window.RootViewController = this.splitView;
        }

        internal override void AddVisualChildren(List<IVisualTreeElement> elements)
        {
            if (this.menu is not null && this.menu is IVisualTreeElement element)
            {
                elements.AddRange(element.GetVisualChildren().ToList());
            }
        }
    }
}