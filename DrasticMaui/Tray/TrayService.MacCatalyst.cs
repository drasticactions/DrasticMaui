// <copyright file="TrayService.MacCatalyst.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using CoreFoundation;
using Foundation;
using ObjCRuntime;

namespace DrasticMaui.Tray
{
    public partial class TrayService : NSObject
    {
        private NSObject? systemStatusBarObj;
        private NSObject? statusBarObj;
        private NSObject? statusBarItem;
        private NSObject? statusBarButton;
        private NSObject? statusBarImage;
        private NSObject? popoverObj;
        private NSObject? nsViewControllerObj;
        private NSObject? nsViewController;

        public Action? ClickHandler { get; set; }

        public void SetupTrayIcon()
        {
            this.statusBarObj = Runtime.GetNSObject(Class.GetHandle("NSStatusBar"));
            if (this.statusBarObj is null)
            {
                return;
            }

            this.popoverObj = Runtime.GetNSObject(IntPtr_objc_msgSend(ObjCRuntime.Class.GetHandle("NSPopover"), Selector.GetHandle("alloc")));
            if (this.popoverObj is null)
            {
                return;
            }

            this.nsViewControllerObj = Runtime.GetNSObject(IntPtr_objc_msgSend(ObjCRuntime.Class.GetHandle("NSViewController"), Selector.GetHandle("alloc")));
            if (this.nsViewControllerObj is null)
            {
                return;
            }

            this.systemStatusBarObj = this.statusBarObj.PerformSelector(new Selector("systemStatusBar"));
            this.statusBarItem = Runtime.GetNSObject(IntPtr_objc_msgSend_nfloat(this.systemStatusBarObj.Handle, Selector.GetHandle("statusItemWithLength:"), -1));
            if (this.statusBarItem is null)
            {
                return;
            }

            this.statusBarButton = Runtime.GetNSObject(IntPtr_objc_msgSend(this.statusBarItem.Handle, Selector.GetHandle("button")));
            this.statusBarImage = Runtime.GetNSObject(IntPtr_objc_msgSend(ObjCRuntime.Class.GetHandle("NSImage"), Selector.GetHandle("alloc")));

            if (this.statusBarImage is null)
            {
                return;
            }

            if (this.statusBarButton is null)
            {
                return;
            }

            var imageStream = Foundation.NSData.FromStream(this.iconStream);
            if (imageStream is null)
            {
                return;
            }

            var nsImagePtr = IntPtr_objc_msgSend_IntPtr(this.statusBarImage.Handle, Selector.GetHandle("initWithData:"), imageStream.Handle);
            void_objc_msgSend_IntPtr(this.statusBarButton.Handle, Selector.GetHandle("setImage:"), this.statusBarImage.Handle);
            void_objc_msgSend_bool(nsImagePtr, Selector.GetHandle("setTemplate:"), true);

            // Handle click
            void_objc_msgSend_IntPtr(this.statusBarButton.Handle, Selector.GetHandle("setTarget:"), this.Handle);
            void_objc_msgSend_IntPtr(this.statusBarButton.Handle, Selector.GetHandle("setAction:"), new Selector("handleButtonClick:").Handle);
        }

        [Export("handleButtonClick:")]
        void HandleClick(NSObject senderStatusBarButton)
        {
            var nsapp = Runtime.GetNSObject(Class.GetHandle("NSApplication"));
            if (nsapp is null)
            {
                return;
            }

            var sharedApp = nsapp.PerformSelector(new Selector("sharedApplication"));

            void_objc_msgSend_bool(sharedApp.Handle, Selector.GetHandle("activateIgnoringOtherApps:"), true);

            this.ToggleWindow();
        }

        private void ToggleWindow()
        {
            if (this.trayWindow is null)
            {
                return;
            }

            if (this.trayWindow.IsVisible)
            {
                this.trayWindow.IsVisible = false;
                Microsoft.Maui.Controls.Application.Current?.CloseWindow(this.trayWindow);
            }
            else
            {
                this.trayWindow.IsVisible = true;
                Microsoft.Maui.Controls.Application.Current?.OpenWindow(this.trayWindow);
            }
        }

        public void SetupPage(Microsoft.Maui.Controls.Page page)
        {
            this.trayWindow = new DrasticTrayWindow() { Page = page };
        }

        [DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
        private static extern IntPtr IntPtr_objc_msgSend_nfloat(IntPtr receiver, IntPtr selector, nfloat arg1);

        [DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
        private static extern IntPtr IntPtr_objc_msgSend_IntPtr(IntPtr receiver, IntPtr selector, IntPtr arg1);

        [DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
        private static extern IntPtr IntPtr_objc_msgSend(IntPtr receiver, IntPtr selector);

        [DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
        private static extern void void_objc_msgSend_IntPtr(IntPtr receiver, IntPtr selector, IntPtr arg1);

        [DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
        private static extern void void_objc_msgSend_bool(IntPtr receiver, IntPtr selector, bool arg1);
    }
}