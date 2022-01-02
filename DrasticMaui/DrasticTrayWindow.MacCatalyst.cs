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

        /// <summary>
        /// Gets a value indicating whether the tray window should be visible.
        /// </summary>
        public bool IsVisible => true;

        private async void SetupWindow()
        {
            var handler = this.Handler as Microsoft.Maui.Handlers.WindowHandler;
            if (handler?.NativeView is not UIKit.UIWindow uiWindow)
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

        protected override void OnHandlerChanged()
        {
            base.OnHandlerChanged();
            this.SetupWindow();
            this.SetupTrayIcon();
        }

        private void TestUIWindowToTray()
        {
            //if (this.statusBarButton is null)
            //{
            //    return;
            //}

            //var nsWindow = window.GetNSWindowFromUIWindow();
            //if (nsWindow is null)
            //{
            //    return;
            //}

            //var buttonWindow = Runtime.GetNSObject(PlatformExtensions.IntPtr_objc_msgSend(this.statusBarButton.Handle, Selector.GetHandle("window")));
            //if (buttonWindow is null)
            //{
            //    return;
            //}

            //var cgRectWindowFrame = Runtime.GetNSObject(PlatformExtensions.IntPtr_objc_msgSend(buttonWindow.Handle, Selector.GetHandle("frame")));
            //if (cgRectWindowFrame is null)
            //{
            //    return;
            //}

            //PlatformExtensions.void_objc_msgSend_IntPtr_bool(nsWindow.Handle, Selector.GetHandle("setFrame:display:"), cgRectWindowFrame.Handle, false);
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