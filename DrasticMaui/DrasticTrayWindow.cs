// <copyright file="DrasticTrayWindow.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrasticMaui
{
    /// <summary>
    /// Drastic Tray Window.
    /// </summary>
    public partial class DrasticTrayWindow : DrasticMauiWindow
    {
        /// <summary>
        /// Gets or sets a value indicating whether the tray window should be visible.
        /// </summary>
        public bool IsVisible { get; set; }
    }
}
