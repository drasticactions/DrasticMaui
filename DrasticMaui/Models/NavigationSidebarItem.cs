// <copyright file="NavigationSidebarItem.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DrasticMaui.Models
{
    /// <summary>
    /// Drastic Menu Item.
    /// </summary>
    public class NavigationSidebarItem
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
        public NavigationSidebarItem(
            string title,
            Guid? id = null,
            string subtitle = "",
            Stream? imageStream = null,
            SidebarItemType type = SidebarItemType.Row)
        {
            this.Id = id ?? Guid.NewGuid();
            this.Type = type;
            this.Title = title;
            this.Subtitle = subtitle;
#if __IOS__
            if (imageStream is not null)
            {
                var imageData = Foundation.NSData.FromStream(imageStream);
                if (imageData is not null)
                {
                    this.Image = UIKit.UIImage.LoadFromData(imageData);
                }

                imageStream.Seek(0, SeekOrigin.Begin);
            }
#else
            this.Image = imageStream;
#endif
        }

        /// <summary>
        /// Gets the Id of the menu item.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the type of sidebar item.
        /// </summary>
        public SidebarItemType Type { get; }

#if __IOS__
        /// <summary>
        /// Gets the image.
        /// </summary>
        public UIKit.UIImage? Image { get; private set; }
#else
        /// <summary>
        /// Gets the sidebar image, optional.
        /// </summary>
        public Stream? Image { get; }
#endif

        /// <summary>
        /// Gets the text for the menu item.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the subtitle text for the menu item.
        /// Optional.
        /// </summary>
        public string? Subtitle { get; }
    }
}