// <copyright file="DrasticSlider.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrasticMaui.Controls
{
    /// <summary>
    /// Drastic Slider.
    /// </summary>
    public partial class DrasticSlider : Slider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrasticSlider"/> class.
        /// </summary>
        public DrasticSlider()
        {
            this.DragCompleted += this.DrasticSlider_DragCompleted;
        }

        /// <summary>
        /// Position Changed.
        /// </summary>
        public event EventHandler<Events.DrasticSliderPositionChangedEventArgs>? NewPositionRequested;

        private void DrasticSlider_DragCompleted(object? sender, EventArgs e)
        {
            if (sender == null)
            {
                return;
            }

            this.NewPositionRequested?.Invoke(this, new Events.DrasticSliderPositionChangedEventArgs((float)this.Value));
        }
    }
}