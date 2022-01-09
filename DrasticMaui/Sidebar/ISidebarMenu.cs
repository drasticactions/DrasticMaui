// <copyright file="ISidebarMenu.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using DrasticMaui.Events;

namespace DrasticMaui
{
    public interface ISidebarMenu
    {
        event EventHandler<DrasticSidebarMenuItemSelectedEventArgs>? OnMenuItemSelect;

        event EventHandler OnSettingsSelect;
    }
}