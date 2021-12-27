// <copyright file="DrasticMenuItem.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;

namespace DrasticMaui.Models
{
    public class DrasticMenuItem
#if __IOS__ || __MACCATALYST__
    : Foundation.NSObject
#endif
    {
        public DrasticMenuItem(string title, Guid? id = null, string subtitle = "", Stream? imageStream = null, SidebarMenuItemType type = SidebarMenuItemType.Row)
        {
            this.Id = id ?? Guid.NewGuid();
            this.Type = type;
            this.Title = title;
            this.Image = imageStream;
            this.Subtitle = subtitle;
        }

        public Guid Id { get; }

        public SidebarMenuItemType Type { get; }

        public Stream? Image { get; }

        /// <summary>
        /// Gets or sets the text for the menu item.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the subtitle text for the menu item.
        /// </summary>
        public string Subtitle { get; set; }
    }
}