// <copyright file="DrasticTrayMenuClickedEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrasticMaui.Events
{
    /// <summary>
    /// Drastic Tray Menu Clicked.
    /// </summary>
    public class DrasticTrayMenuClickedEventArgs : EventArgs
    {
        /// <summary>
        /// DrasticTrayMenuItem.
        /// </summary>
        public readonly DrasticTrayMenuItem MenuItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrasticTrayMenuClickedEventArgs"/> class.
        /// </summary>
        /// <param name="item">Position.</param>
        internal DrasticTrayMenuClickedEventArgs(DrasticTrayMenuItem item)
        {
            this.MenuItem = item;
        }
    }
}
