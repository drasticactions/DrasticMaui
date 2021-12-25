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

        private void NotifyIcon_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button is MouseButtons.Left)
            {

            }
        }
    }
}