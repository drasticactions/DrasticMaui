// <copyright file="DrasticTrayIcon.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DrasticMaui.Events;
using System;

namespace DrasticMaui
{
    public partial class DrasticTrayIcon
    {
        private Stream? iconStream;
        private string? iconName;
        private List<DrasticTrayMenuItem> menuItems;

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