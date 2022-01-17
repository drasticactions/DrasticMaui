// <copyright file="DrasticMauiWindow.MacCatalyst.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DrasticMaui.Tools;
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

            window.ToggleFullScreen(fullScreen);
        }
    }
}
