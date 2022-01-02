// <copyright file="DrasticTrayWindow.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrasticMaui.Options;

namespace DrasticMaui
{
    /// <summary>
    /// Drastic Tray Window.
    /// </summary>
    public partial class DrasticTrayWindow : DrasticMauiWindow
    {
        private DrasticTrayIcon icon;
        private DrasticTrayWindowOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrasticTrayWindow"/> class.
        /// </summary>
        /// <param name="icon"><see cref="DrasticTrayIcon"/>.</param>
        /// <param name="options"><see cref="DrasticTrayWindowOptions"/>.</param>
        public DrasticTrayWindow(DrasticTrayIcon icon, DrasticTrayWindowOptions? options = null)
        {
            this.icon = icon;
            this.options = options ?? new DrasticTrayWindowOptions();
        }

        private void SetupTrayIcon()
        {
            if (this.icon is null)
            {
                return;
            }

            this.icon.LeftClicked += this.Icon_LeftClicked;
        }

        private void Icon_LeftClicked(object? sender, EventArgs e)
        {
            if (this.IsVisible)
            {
                this.HideWindow();
            }
            else
            {
                this.ShowWindow();
            }
        }

#if !WINDOWS && !MACCATALYST

        /// <summary>
        /// Gets a value indicating whether the tray window should be visible.
        /// </summary>
        public bool IsVisible => true;

        private void SetupWindow()
        {
        }

        private void ShowWindow()
        {
        }

        private void HideWindow()
        {
        }
#endif
    }
}
