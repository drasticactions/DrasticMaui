// <copyright file="DrasticTrayIcon.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using DrasticMaui.Events;
using Microsoft.Maui.Handlers;

namespace DrasticMaui
{
    public partial class DrasticTrayIcon : IDisposable
    {
        private Stream? iconStream;
        private string? iconName;
        private List<DrasticTrayMenuItem> menuItems;
        private bool holdsWindowInTray;
        private bool disposedValue;

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

        private void NativeElementDispose()
        {
        }
#endif

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.NativeElementDispose();
                }

                this.disposedValue = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}