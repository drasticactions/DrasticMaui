// <copyright file="DrasticMauiWindow.Windows.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Windowing;

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
            var handler = this.Handler as Microsoft.Maui.Handlers.WindowHandler;
            if (handler?.NativeView is not Microsoft.UI.Xaml.Window window)
            {
                return;
            }

            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            var myWndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            var win = AppWindow.GetFromWindowId(myWndId);

            if (win is not null)
            {
                if (fullScreen)
                {
                    win.SetPresenter(AppWindowPresenterKind.FullScreen);
                }
                else
                {
                    win.SetPresenter(AppWindowPresenterKind.Default);
                }
            }
        }
    }
}
