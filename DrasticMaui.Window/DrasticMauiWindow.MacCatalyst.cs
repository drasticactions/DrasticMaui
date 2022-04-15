// <copyright file="DrasticMauiWindow.MacCatalyst.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

#pragma warning disable SA1210 // Using directives need to be in a specific order for MAUI
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Platform;
using DrasticMaui.Tools;
using Foundation;
using ObjCRuntime;
using UIKit;
#pragma warning restore SA1210 // Using directives can't be ordered alphabetically by namespace

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
            UIWindow? window = this.Handler?.PlatformView as UIWindow;

            if (window is null)
            {
                return;
            }

            window.ToggleFullScreen(fullScreen);
        }
    }
}
