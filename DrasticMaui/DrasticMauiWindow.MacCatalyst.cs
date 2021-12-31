// <copyright file="DrasticMauiWindow.MacCatalyst.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace DrasticMaui
{
    /// <summary>
    /// Drastic Maui Window.
    /// </summary>
    public partial class DrasticMauiWindow
    {
        /// <summary>
        /// Toggle Full Screen Support.
        /// </summary>
        /// <param name="fullScreen">Enable Full Screen.</param>
        public void ToggleFullScreen(bool fullScreen)
        {
            var window = this.Handler?.NativeView as UIWindow;
            if (window is null)
            {
                return;
            }

            // TODO: Get NSWindow.

            var nsApplication = Runtime.GetNSObject(Class.GetHandle("NSApplication"));
            if (nsApplication is null)
            {
                return;
            }

            var sharedApplication = nsApplication.PerformSelector(new Selector("sharedApplication"));
            if (sharedApplication is null)
            {
                return;
            }

            var applicationDelegate = sharedApplication.PerformSelector(new Selector("delegate"));
            if (applicationDelegate is null)
            {
                return;
            }

            var nsWindowHandle = IntPtr_objc_msgSend_IntPtr(applicationDelegate.Handle, Selector.GetHandle("hostWindowForUIWindow:"), window.Handle);
            var nsWindow = Runtime.GetNSObject(nsWindowHandle);
            if (nsWindow is null)
            {
                return;
            }

            void_objc_msgSend_bool(nsWindowHandle, Selector.GetHandle("toggleFullScreen:"), fullScreen);
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
