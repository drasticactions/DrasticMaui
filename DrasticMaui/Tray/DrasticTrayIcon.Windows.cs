// <copyright file="DrasticTrayIcon.Windows.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrasticMaui.Events;
using DrasticMaui.Tools;
using Microsoft.Maui.Handlers;
using Microsoft.UI.Windowing;
using Windows.Graphics;

namespace DrasticMaui
{
    /// <summary>
    /// Drastic Tray Icon.
    /// </summary>
    public partial class DrasticTrayIcon
    {
        private System.Windows.Forms.ContextMenuStrip? contextMenuStrip;
        private NotifyIcon? notifyIcon;
        private Icon? icon;
        private Microsoft.UI.Windowing.AppWindow? appWindow;

        public void MoveWindowToTray(WindowHandler handler)
        {
            if (this.notifyIcon is null)
            {
                return;
            }

            if (handler.NativeView is not Microsoft.UI.Xaml.Window window)
            {
                return;
            }

            if (this.appWindow is null)
            {
                this.appWindow = window.GetAppWindowForWinUI();
                if (this.appWindow.Presenter is OverlappedPresenter presenter)
                {
                    presenter.IsResizable = false;
                    presenter.IsAlwaysOnTop = true;
                    presenter.IsMaximizable = false;
                    presenter.IsMinimizable = false;
                }

                this.appWindow.TitleBar.SetDragRectangles(new[] { new RectInt32(0, 0, 0, 0) });
                this.appWindow.Hide();
               // this.appWindow.SetPresenter()
            }

            this.holdsWindowInTray = true;
        }

        private void SetupStatusBarButton()
        {
            this.notifyIcon = new NotifyIcon();
            this.notifyIcon.Icon = this.icon;
            this.notifyIcon.Text = this.iconName;
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += this.NotifyIcon_MouseClick;
        }

        private void NotifyIcon_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.holdsWindowInTray && this.appWindow is not null)
                {
                    this.HandleWindow(this.appWindow, Cursor.Position);
                }

                this.LeftClicked?.Invoke(this, EventArgs.Empty);
            }

            if (e.Button == MouseButtons.Right)
            {
                this.RightClicked?.Invoke(this, EventArgs.Empty);
            }
        }

        private void HandleWindow(Microsoft.UI.Windowing.AppWindow window, System.Drawing.Point point)
        {
            if (!window.IsVisible)
            {
                window.MoveAndResize(new Windows.Graphics.RectInt32(point.X - 200, point.Y - 650, 400, 600));
                window.Show();
            }
            else
            {
                window.Hide();
            }
        }

        private void SetupStatusBarImage()
        {
            if (this.iconStream is not null)
            {
                this.icon = new Icon(this.iconStream);
            }
        }

        private void SetupStatusBarMenu()
        {
            if (!this.menuItems.Any())
            {
                return;
            }

            if (this.notifyIcon is null)
            {
                return;
            }

            this.contextMenuStrip = new ContextMenuStrip();
            this.contextMenuStrip.ItemClicked += this.ContextMenuStrip_ItemClicked;
            var items = this.menuItems.Select(n => this.GenerateItem(n)).Reverse().ToArray();
            this.contextMenuStrip.Items.AddRange(items);
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
        }

        private void ContextMenuStrip_ItemClicked(object? sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem is DrasticToolStripMenuItem stripItem)
            {
                this.MenuClicked?.Invoke(this, new DrasticTrayMenuClickedEventArgs(stripItem.Item));
            }
        }

        private DrasticToolStripMenuItem GenerateItem(DrasticTrayMenuItem item)
        {
            var menu = new DrasticToolStripMenuItem(item);
            menu.Text = item.Text;
            if (item.Icon is not null)
            {
                menu.Image = System.Drawing.Image.FromStream(item.Icon);
            }

            return menu;
        }

        private class DrasticToolStripMenuItem : ToolStripMenuItem
        {
            public DrasticToolStripMenuItem(DrasticTrayMenuItem item)
            {
                this.Text = item.Text;
                if (item.Icon is not null)
                {
                    this.Image = System.Drawing.Image.FromStream(item.Icon);
                }

                this.Item = item;
            }

            public DrasticTrayMenuItem Item { get; }
        }
    }
}
