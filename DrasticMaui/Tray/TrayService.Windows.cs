// <copyright file="TrayService.Windows.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Windows.Forms;

namespace DrasticMaui.Tray
{
    public partial class TrayService
    {
        private NotifyIcon? notifyIcon;

        public void SetupTrayIcon()
        {
            this.notifyIcon = new NotifyIcon();
            this.notifyIcon.Icon = new Icon(this.iconStream);
            this.notifyIcon.Text = this.iconName;
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += this.NotifyIcon_MouseClick;
        }

        public void SetupPage(Microsoft.Maui.Controls.Page page)
        {
            this.trayWindow = new DrasticTrayWindow() { Page = page };
        }

        private void ToggleWindow()
        {
            if (this.trayWindow is null)
            {
                return;
            }

            if (this.trayWindow.IsVisible)
            {
                this.trayWindow.IsVisible = false;
                Microsoft.Maui.Controls.Application.Current?.CloseWindow(this.trayWindow);
            }
            else
            {
                this.trayWindow.IsVisible = true;
                Microsoft.Maui.Controls.Application.Current?.OpenWindow(this.trayWindow);
            }
        }

        private void NotifyIcon_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button is MouseButtons.Left)
            {
                this.ToggleWindow();
            }
        }
    }
}