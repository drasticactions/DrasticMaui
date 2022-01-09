// <copyright file="DrasticSidebarMenuItemSelectedEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using DrasticMaui.Models;

namespace DrasticMaui.Events
{
    public class DrasticSidebarMenuItemSelectedEventArgs : EventArgs
    {
        private readonly NavigationSidebarItem item;

        public DrasticSidebarMenuItemSelectedEventArgs(NavigationSidebarItem item)
        {
            this.item = item;
        }

        public NavigationSidebarItem Item => this.item;
    }
}