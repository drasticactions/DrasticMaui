// <copyright file="DrasticMauiSampleWindow.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DrasticMaui.Models;
using DrasticMaui.Overlays;
using Microsoft.Maui.Handlers;

namespace DrasticMaui.Sample
{
    /// <summary>
    /// Drastic Maui Window.
    /// </summary>
    public class DrasticMauiSampleWindow : DrasticMauiWindow
    {
        public DrasticMauiSampleWindow()
        {
            this.DragAndDropOverlay = new DragAndDropOverlay(this);
            this.PageOverlay = new PageOverlay(this);
        }

        internal DrasticTrayIcon? TrayIcon { get; private set; }

        /// <summary>
        /// Gets the drag and drop overlay.
        /// </summary>
        internal DragAndDropOverlay DragAndDropOverlay { get; }

        /// <summary>
        /// Gets the page overlay.
        /// </summary>
        internal PageOverlay PageOverlay { get; }

        /// <inheritdoc/>
        protected override void OnCreated()
        {
            this.AddOverlay(this.PageOverlay);
            this.AddOverlay(this.DragAndDropOverlay);
        }

        public void SetupTrayIcon()
        {
            if (this.TrayIcon is not null)
            {
                return;
            }

            var stream = GetResourceFileContent("Icon.favicon.ico");
            if (stream is null)
            {
                throw new Exception("Couldn't set up tray image");
            }

            var menuItems = new List<DrasticTrayMenuItem>
            {
                new DrasticTrayMenuItem("Exit"),
                new DrasticTrayMenuItem("Test"),
                new DrasticTrayMenuItem("Test 2")
            };

            this.TrayIcon = new DrasticTrayIcon("Maui", stream, menuItems);
            this.TrayIcon.LeftClicked += this.TrayIcon_Clicked;
            this.TrayIcon.MenuClicked += TrayIcon_MenuClicked;
        }

        private void TrayIcon_MenuClicked(object? sender, Events.DrasticTrayMenuClickedEventArgs e)
        {
            if (e.MenuItem.Text == "Exit")
            {
#if WINDOWS
                Microsoft.UI.Xaml.Application.Current.Exit();
                return;
#endif
            }
        }

        private void TrayIcon_Clicked(object? sender, EventArgs e)
        {
        }

        private static Stream? GetResourceFileContent(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "DrasticMaui.Sample." + fileName;
            if (assembly is null)
            {
                return null;
            }

            return assembly.GetManifestResourceStream(resourceName);
        }
    }
}
