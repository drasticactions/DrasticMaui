// <copyright file="DrasticTrayWindow.MacCatalyst.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreAnimation;
using CoreGraphics;
using DrasticMaui.Tools;
using Foundation;
using Microsoft.Maui.Handlers;
using ObjCRuntime;
using UIKit;

namespace DrasticMaui
{
    /// <summary>
    /// Drastic Tray Window.
    /// </summary>
    public partial class DrasticTrayWindow
    {
        private UIKit.UIWindow? uiWindow;
        private NSObject? nsWindow;
        private bool isActivated;
        private DrasticTrayUIViewController? drasticViewController;

        /// <inheritdoc/>
        protected override void OnHandlerChanged()
        {
            base.OnHandlerChanged();
            this.SetupWindow();
            this.SetupTrayIcon();
        }

        private async void SetupWindow()
        {
            var handler = this.Handler as Microsoft.Maui.Handlers.WindowHandler;
            if (handler?.PlatformView is not UIKit.UIWindow uiWindow)
            {
                return;
            }

            this.uiWindow = uiWindow;

            this.nsWindow = await this.uiWindow.GetNSWindowFromUIWindow();

            await this.uiWindow.ToggleTitleBarButtons(true);

            if (this.uiWindow.RootViewController is null)
            {
                return;
            }

            this.uiWindow.RootViewController = this.drasticViewController = new DrasticTrayUIViewController(this.uiWindow, this.uiWindow.RootViewController, this.icon, this.options);
        }

        private void ShowWindow()
        {
            this.drasticViewController?.ToggleVisibility();
        }

        private void HideWindow()
        {
            this.drasticViewController?.ToggleVisibility();
        }
    }
}