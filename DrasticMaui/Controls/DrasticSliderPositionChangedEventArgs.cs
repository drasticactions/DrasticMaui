// <copyright file="DrasticSliderPositionChangedEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DrasticMaui.Events
{
    /// <summary>
    /// The Drastic Sliders's position changed.
    /// </summary>
    public class DrasticSliderPositionChangedEventArgs : EventArgs
    {
        private readonly float position;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrasticSliderPositionChangedEventArgs"/> class.
        /// </summary>
        /// <param name="position">Position.</param>
        internal DrasticSliderPositionChangedEventArgs(float position)
        {
            this.position = position;
        }

        /// <summary>
        /// Gets the Drastic Sliders's current position.
        /// </summary>
        public float Position => this.position;
    }
}