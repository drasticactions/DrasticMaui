// <copyright file="DrasticTrayMenuItem.cs" company="Drastic Actions">
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
    /// Drastic Tray Menu Item.
    /// </summary>
    public class DrasticTrayMenuItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrasticTrayMenuItem"/> class.
        /// </summary>
        /// <param name="text">Menu Text.</param>
        /// <param name="icon">Icon.</param>
        /// <param name="action">Action to perform when clicked.</param>
        public DrasticTrayMenuItem (string text, Stream? icon = null, Task<Action>? action = null)
        {
            this.Text = text;
            this.Icon = icon;
            this.Action = action;
        }

        /// <summary>
        /// Gets the text for the menu item.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets the icon for the menu item.
        /// Optional.
        /// </summary>
        public Stream? Icon { get; }

        /// <summary>
        /// Gets the action to be performed when the item is clicked.
        /// Optional.
        /// </summary>
        public Task<Action>? Action { get; }
    }
}
