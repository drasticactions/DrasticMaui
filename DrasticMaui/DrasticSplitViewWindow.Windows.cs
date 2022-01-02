// <copyright file="DrasticSplitViewWindow.Windows.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrasticMaui.Tools;

namespace DrasticMaui
{
    /// <summary>
    /// Drastic Split View Window.
    /// </summary>
    public partial class DrasticSplitViewWindow
    {
        private Microsoft.UI.Xaml.Controls.SplitView? splitView;

        /// <summary>
        /// Setup Split View Window.
        /// </summary>
        public void SetupSplitView()
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

            this.splitView = new Microsoft.UI.Xaml.Controls.SplitView();

            if (this.Handler.MauiContext is not null)
            {
                var testing = this.menu.ToHandler(this.Handler.MauiContext);
                this.splitView.Pane = testing.NativeView;
            }

            this.splitView.DisplayMode = Microsoft.UI.Xaml.Controls.SplitViewDisplayMode.Inline;
            this.splitView.Content = this.Page.GetNative(true);

            // TODO: Keep existing overlays.
            panel.Children.Clear();
            panel.Children.Add(this.splitView);

            window.ExtendsContentIntoTitleBar = false;
            this.splitView.IsPaneOpen = true;
        }
    }
}
