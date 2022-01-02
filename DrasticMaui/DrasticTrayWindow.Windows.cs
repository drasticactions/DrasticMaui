// <copyright file="DrasticTrayWindow.Windows.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrasticMaui.Tools;
using Microsoft.UI.Windowing;
using Windows.Graphics;

namespace DrasticMaui
{
    /// <summary>
    /// Drastic Tray Window.
    /// </summary>
    public partial class DrasticTrayWindow
    {
        private Microsoft.UI.Windowing.AppWindow? appWindow;
        private bool appLaunched;

        ///// <inheritdoc/>
        protected override void OnCreated()
        {
            base.OnCreated();
            this.SetupWindow();
            this.SetupTrayIcon();
        }

        private void SetupWindow()
        {
            var handler = this.Handler as Microsoft.Maui.Handlers.WindowHandler;
            if (handler?.NativeView is not Microsoft.UI.Xaml.Window window)
            {
                return;
            }

            this.appWindow = window.GetAppWindowForWinUI();
            if (this.appWindow.Presenter is OverlappedPresenter presenter)
            {
                presenter.IsResizable = false;
                presenter.IsAlwaysOnTop = true;
                presenter.IsMaximizable = false;
                presenter.IsMinimizable = false;
            }

            window.VisibilityChanged += this.Window_VisibilityChanged;

            this.appWindow.TitleBar.SetDragRectangles(new[] { new RectInt32(0, 0, 0, 0) });
            this.appWindow.Hide();
        }

        private void Window_VisibilityChanged(object sender, Microsoft.UI.Xaml.WindowVisibilityChangedEventArgs args)
        {
            if (!this.appLaunched)
            {
                this.appWindow?.Hide();
                this.appLaunched = true;
            }

            if (sender is Microsoft.UI.Xaml.Window window)
            {
                window.VisibilityChanged -= this.Window_VisibilityChanged;
            }
        }

        private void ShowWindow()
        {
            if (this.appWindow is null)
            {
                return;
            }

            // The cursor is, most likely, positioned next to the tray icon.
            var point = Cursor.Position;
            var x = point.X - (this.options.WindowWidth / 2);
            var y = point.Y - (this.options.WindowHeight + 50);
            var width = this.options.WindowWidth;
            var height = this.options.WindowHeight;

            this.appWindow.MoveAndResize(new Windows.Graphics.RectInt32(x, y, width, height));
            this.appWindow.Show();
        }

        private void HideWindow()
        {
            this.appWindow?.Hide();
        }
    }
}
