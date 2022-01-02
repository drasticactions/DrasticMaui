// <copyright file="DrasticMenuItem.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;

namespace DrasticMaui.Models
{
    /// <summary>
    /// Drastic Menu Item.
    /// </summary>
    public class DrasticMenuItem
#if __IOS__ || __MACCATALYST__
    : Foundation.NSObject
#endif
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrasticMenuItem"/> class.
        /// </summary>
        /// <param name="title">Title.</param>
        /// <param name="action">Action.</param>
        /// <param name="page">Page.</param>
        /// <param name="id">Id. Auto Generated.</param>
        /// <param name="subtitle">Subtitle.</param>
        /// <param name="imageStream">Icon.</param>
        /// <param name="type">Type of Sidebar Item.</param>
        public DrasticMenuItem(
            string title,
            Task<Action>? action = null,
            Page? page = null,
            Guid? id = null,
            string subtitle = "",
            Stream? imageStream = null,
            SidebarMenuItemType type = SidebarMenuItemType.Row)
        {
            this.Id = id ?? Guid.NewGuid();
            this.Action = action;
            this.Page = page;

            if (this.Action is null && this.Page is null)
            {
                throw new ArgumentNullException("Must set either Action or Page.");
            }

            this.Type = type;
            this.Title = title;
            this.Image = imageStream;
            this.Subtitle = subtitle;
        }

        /// <summary>
        /// Gets the Id of the menu item.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the type of sidebar item.
        /// </summary>
        public SidebarMenuItemType Type { get; }

        /// <summary>
        /// Gets the sidebar image, optional.
        /// </summary>
        public Stream? Image { get; }

        /// <summary>
        /// Gets the text for the menu item.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the subtitle text for the menu item.
        /// Optional.
        /// </summary>
        public string? Subtitle { get; }

        /// <summary>
        /// Gets the page to navigate to.
        /// </summary>
        public Page? Page { get; }

        /// <summary>
        /// Gets the action to invoke upon selection.
        /// </summary>
        public Task<Action>? Action { get; }
    }
}