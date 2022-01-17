// <copyright file="DrasticTrayIcon.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using DrasticMaui.Events;
using Microsoft.Maui.Handlers;

namespace DrasticMaui
{
    /// <summary>
    /// Drastic Tray Icon.
    /// </summary>
    public partial class DrasticTrayIcon : IDisposable
    {
        private Stream? iconStream;
        private string? iconName;
        private List<DrasticTrayMenuItem> menuItems;
        private bool holdsWindowInTray;
        private bool disposedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrasticTrayIcon"/> class.
        /// </summary>
        /// <param name="name">Name of the icon.</param>
        /// <param name="stream">Icon Image Stream. Optional.</param>
        /// <param name="menuItems">Items to populate context menu. Optional.</param>
        public DrasticTrayIcon(string name, Stream? stream = null, List<DrasticTrayMenuItem>? menuItems = null)
        {
            this.menuItems = menuItems ?? new List<DrasticTrayMenuItem>();
            this.iconName = name;
            this.iconStream = stream;
            this.SetupStatusBarImage();
            this.SetupStatusBarButton();
            this.SetupStatusBarMenu();
        }

        /// <summary>
        /// Left Clicked Event.
        /// </summary>
        public event EventHandler<EventArgs>? LeftClicked;

        /// <summary>
        /// Right Clicked Event.
        /// </summary>
        public event EventHandler<EventArgs>? RightClicked;

        /// <summary>
        /// Menu Item Clicked.
        /// </summary>
        public event EventHandler<DrasticTrayMenuClickedEventArgs>? MenuClicked;

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">Is Disposing.</param>
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
    }
}