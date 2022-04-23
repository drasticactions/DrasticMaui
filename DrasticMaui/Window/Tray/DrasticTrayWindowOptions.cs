// <copyright file="DrasticTrayWindowOptions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DrasticMaui
{
    /// <summary>
    /// Drastic Tray Window Options.
    /// </summary>
    public class DrasticTrayWindowOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrasticTrayWindowOptions"/> class.
        /// </summary>
        /// <param name="width">Width of the window, Default: 400.</param>
        /// <param name="height">Height of the window, Default: 600.</param>
        public DrasticTrayWindowOptions(int width = 400, int height = 600)
        {
            if (width <= 0)
            {
                throw new ArgumentException(nameof(width));
            }

            if (height <= 0)
            {
                throw new ArgumentException(nameof(height));
            }

            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Gets the window width.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets the window height.
        /// </summary>
        public int Height { get; }
    }
}