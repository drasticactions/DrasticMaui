// <copyright file="DrasticTrayIcon.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using DrasticMaui.Events;
using Microsoft.Maui.Handlers;

namespace DrasticMaui
{
    public partial class DrasticTrayIcon
    {
        private Stream? iconStream;
        private string? iconName;
        private List<DrasticTrayMenuItem> menuItems;
        private bool holdsWindowInTray;

        public DrasticTrayIcon(string name, Stream stream, List<DrasticTrayMenuItem>? menuItems = null)
        {
            this.menuItems = menuItems ?? new List<DrasticTrayMenuItem>();
            this.iconName = name;
            this.iconStream = stream;
            this.SetupStatusBarImage();
            this.SetupStatusBarButton();
            this.SetupStatusBarMenu();
        }

        public event EventHandler<EventArgs>? LeftClicked;

        public event EventHandler<EventArgs>? RightClicked;

        public event EventHandler<DrasticTrayMenuClickedEventArgs>? MenuClicked;

#if !__MACCATALYST__ && !WINDOWS

        public void MoveWindowToTray(WindowHandler handler)
        {
        }

        private void SetupStatusBarButton()
        {
        }

        private void SetupStatusBarImage()
        {
        }

        private void SetupStatusBarMenu()
        {
        }
#endif
    }
}