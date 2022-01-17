// <copyright file="DrasticMauiSampleWindow.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DrasticMaui.Models;
using DrasticMaui.Overlays;
using DrasticMaui.Tools;
using Microsoft.Maui.Handlers;
using System.Reflection;

namespace DrasticMaui.Sample
{
    /// <summary>
    /// Drastic Maui Window.
    /// </summary>
    public class DrasticMauiSampleWindow : DrasticSideBarNavigationWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrasticMauiSampleWindow"/> class.
        /// </summary>
        /// <param name="services">Service Provider.</param>
        public DrasticMauiSampleWindow(SidebarMenuOptions options, IServiceProvider services)
            : base(options, services)
        {
            this.DragAndDropOverlay = new DragAndDropOverlay(this);
            this.PageOverlay = new PageOverlay(this);
        }

        /// <summary>
        /// Gets the tray icon.
        /// </summary>
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

        public async Task TestTrayIcon()
        {
#if __MACCATALYST__
            if (this.Handler is WindowHandler handler)
            {
                await handler.NativeView.SetFrameForUIWindow(new CoreGraphics.CGRect(0, 0, 100, 100));
            }
#endif
        }

        public async Task EnableTrayApp()
        {
            if (this.TrayIcon is not null)
            {
                var trayWindow = new DrasticTrayWindow(this.ServiceProvider, this.TrayIcon) { Page = new TraySample() };
                Application.Current?.OpenWindow(trayWindow);
            }
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
                new DrasticTrayMenuItem("Exit", action: async () => this.ExitApp()),
                new DrasticTrayMenuItem("Test", action: async () => this.ShowMessage("Test Message One")),
                new DrasticTrayMenuItem("Test 2", action: async () => this.ShowMessage("Test Message Two")),
            };

            this.TrayIcon = new DrasticTrayIcon("Maui", stream, menuItems);
            this.TrayIcon.LeftClicked += this.TrayIcon_Clicked;
            this.TrayIcon.MenuClicked += TrayIcon_MenuClicked;
        }

        private async void ShowMessage(string message)
        {
            await this.Page?.DisplayAlert("Message", message, "Okay");
        }

        private async void TrayIcon_MenuClicked(object? sender, Events.DrasticTrayMenuClickedEventArgs e)
        {
            if (e.MenuItem.Action is not null)
            {
                await e.MenuItem.Action();
            }
        }

        private void ExitApp()
        {
#if WINDOWS
            Microsoft.UI.Xaml.Application.Current.Exit();
#endif
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
