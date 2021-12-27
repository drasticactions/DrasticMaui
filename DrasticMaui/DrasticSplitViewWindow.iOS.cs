// <copyright file="DrasticSplitViewWindow.iOS.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using DrasticMaui.iOS;
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

            this.splitView = new UISplitViewController();
            this.splitView.PrimaryBackgroundStyle = UISplitViewControllerBackgroundStyle.Sidebar;
            var a = new DrasticMenuCollectionView() { View = new UIView() };

            var b = this.Page.ToUIViewController(this.context);
            this.splitView.ViewControllers = new UIViewController[] { a, b };

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