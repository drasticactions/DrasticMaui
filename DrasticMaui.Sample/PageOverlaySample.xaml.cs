// <copyright file="PageOverlaySample.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using DrasticMaui.Overlays;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace DrasticMaui.Sample
{
    /// <summary>
    /// Page Overlay Sample.
    /// </summary>
    public partial class PageOverlaySample : ContentPage, IHitTestView
    {
        private DrasticMauiSampleWindow window;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageOverlaySample"/> class.
        /// </summary>
        /// <param name="window">Window.</param>
        public PageOverlaySample(DrasticMauiSampleWindow window)
        {
            this.InitializeComponent();
            this.window = window;
            this.HitTestViews.Add(this.ControlLayout);
        }

        /// <summary>
        /// Gets the hit test views.
        /// </summary>
        public List<IView> HitTestViews { get; } = new List<IView>();

        private void OnPageOverlay(object sender, EventArgs e)
        {
            this.window.PageOverlay.RemoveView(this);
        }
    }
}