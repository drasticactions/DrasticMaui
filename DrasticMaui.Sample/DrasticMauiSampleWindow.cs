// <copyright file="DrasticMauiSampleWindow.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrasticMaui.Overlays;

namespace DrasticMaui.Sample
{
    /// <summary>
    /// Drastic Maui Window.
    /// </summary>
    public class DrasticMauiSampleWindow : DrasticMauiWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrasticMauiSampleWindow"/> class.
        /// </summary>
        public DrasticMauiSampleWindow()
        {
            this.DragAndDropOverlay = new DragAndDropOverlay(this);
            this.PageOverlay = new PageOverlay(this);
        }

        /// <summary>
        /// Gets the drag and drop overlay.
        /// </summary>
        internal DragAndDropOverlay DragAndDropOverlay { get; }

        /// <summary>
        /// Gets the page overlay.
        /// </summary>
        internal PageOverlay PageOverlay { get; }

        /// <inheritdoc/>
        protected override void OnCreated()
        {
            this.AddOverlay(this.PageOverlay);
            this.AddOverlay(this.DragAndDropOverlay);
        }
    }
}
