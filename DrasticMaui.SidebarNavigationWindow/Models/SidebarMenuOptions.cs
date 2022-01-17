// <copyright file="SidebarMenuOptions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;

namespace DrasticMaui.Models
{
    public class SidebarMenuOptions
    {
        public SidebarMenuOptions(string header, IEnumerable<NavigationSidebarItem> items, bool showSettings = false, string settingsTitle = "Settings", bool showSearchBar = false, string searchBarHeader = "Search")
        {
            this.Header = header;
            this.DefaultSidebarItems = items.ToList().AsReadOnly();
            this.ShowSearchBar = showSearchBar;
            this.SearchBarHeader = searchBarHeader;
            this.SettingsTitle = settingsTitle;
            this.ShowSettingsItem = showSettings;
        }

        public IReadOnlyList<NavigationSidebarItem> DefaultSidebarItems { get; }

        public string Header { get; }

        public bool ShowSettingsItem { get; }

        public string SettingsTitle { get; }

        public bool ShowSearchBar { get; }

        public string SearchBarHeader { get; }
    }
}