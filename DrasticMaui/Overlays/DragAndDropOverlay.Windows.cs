// <copyright file="DragAndDropOverlay.Windows.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrasticMaui.Tools;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Streams;

namespace DrasticMaui.Overlays
{
    /// <summary>
    /// Drag And Drop Overlay.
    /// </summary>
    public partial class DragAndDropOverlay
    {
        private Microsoft.UI.Xaml.UIElement? panel;

        /// <inheritdoc/>
        public override bool Initialize()
        {
            if (this.dragAndDropOverlayNativeElementsInitialized)
            {
                return true;
            }

            base.Initialize();

            var nativeElement = this.Window.Content.GetNative(true);
            if (nativeElement == null)
            {
                return false;
            }

            var handler = this.Window.Handler as Microsoft.Maui.Handlers.WindowHandler;
            if (handler?.NativeView is not Microsoft.UI.Xaml.Window window)
            {
                return false;
            }

            this.panel = window.Content as Microsoft.UI.Xaml.Controls.Panel;
            if (this.panel == null)
            {
                return false;
            }

            this.panel.AllowDrop = true;
            this.panel.DragOver += this.Panel_DragOver;
            this.panel.Drop += this.Panel_Drop;
            this.panel.DragLeave += this.Panel_DragLeave;
            this.panel.DropCompleted += this.Panel_DropCompleted;
            return this.dragAndDropOverlayNativeElementsInitialized = true;
        }

        /// <inheritdoc/>
        public override bool Deinitialize()
        {
            if (this.panel != null)
            {
                this.panel.AllowDrop = false;
                this.panel.DragOver -= this.Panel_DragOver;
                this.panel.Drop -= this.Panel_Drop;
                this.panel.DragLeave -= this.Panel_DragLeave;
                this.panel.DropCompleted -= this.Panel_DropCompleted;
            }

            return base.Deinitialize();
        }

        private void Panel_DropCompleted(Microsoft.UI.Xaml.UIElement sender, Microsoft.UI.Xaml.DropCompletedEventArgs args)
        {
            this.IsDragging = false;
        }

        private void Panel_DragLeave(object sender, Microsoft.UI.Xaml.DragEventArgs e)
        {
            this.IsDragging = false;
        }

        private async void Panel_Drop(object sender, Microsoft.UI.Xaml.DragEventArgs e)
        {
            // We're gonna cheat and only take the first item dragged in by the user.
            // In the real world, you would probably want to handle multiple drops and figure
            // Out what to do for your app.
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();
                if (items.Any())
                {
                    var filePaths = new List<string>();
                    foreach (var item in items)
                    {
                        if (item is StorageFile file)
                        {
                            filePaths.Add(item.Path);
                        }
                    }

                    this.Drop?.Invoke(this, new DragAndDropOverlayTappedEventArgs(filePaths));
                }
            }
        }

        private void Panel_DragOver(object sender, Microsoft.UI.Xaml.DragEventArgs e)
        {
            e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Copy;
        }
    }
}