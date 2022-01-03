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
        /// <param name="services">Service Provider.</param>
        /// <param name="icon"><see cref="DrasticTrayIcon"/>.</param>
        /// <param name="options"><see cref="DrasticTrayWindowOptions"/>.</param>
        public DrasticTrayWindow(IServiceProvider provider, DrasticTrayIcon icon, DrasticTrayWindowOptions? options = null)
            : base(provider)
        {
            this.icon = icon;
            this.options = options ?? new DrasticTrayWindowOptions();
        }

        /// <summary>
        /// Gets a value indicating whether the tray window should be visible.
        /// </summary>
        public bool IsVisible { get; private set; }

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

            this.IsVisible = !this.IsVisible;
        }

#if !WINDOWS && !MACCATALYST

        private void ShowWindow()
        {
        }

        private void HideWindow()
        {
        }
#endif
    }
}
