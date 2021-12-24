// <copyright file="PageOverlay.Windows.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DrasticMaui.Tools;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace DrasticMaui.Overlays
{
    /// <summary>
    /// Page Overlay.
    /// </summary>
    public partial class PageOverlay
    {
        private Microsoft.UI.Xaml.Controls.Panel? panel;
        private FrameworkElement? element;

        /// <inheritdoc/>
        public override bool Initialize()
        {
            if (this.pageOverlayNativeElementsInitialized)
            {
                return true;
            }

            var nativeElement = this.Window.Content.GetNative(true);
            if (nativeElement is null)
            {
                return false;
            }

            var handler = this.Window.Handler as Microsoft.Maui.Handlers.WindowHandler;
            if (handler?.NativeView is not Microsoft.UI.Xaml.Window window)
            {
                return false;
            }

            if (handler.MauiContext is null)
            {
                return false;
            }

            this.context = handler.MauiContext;

            this.panel = window.Content as Microsoft.UI.Xaml.Controls.Panel;
            if (this.panel is null)
            {
                return false;
            }

            this.panel.PointerMoved += this.Panel_PointerMoved;
            return this.pageOverlayNativeElementsInitialized = true;
        }

        /// <inheritdoc/>
        public override bool Deinitialize()
        {
            this.RemoveViews();
            if (this.panel is not null)
            {
                this.panel.PointerMoved -= this.Panel_PointerMoved;
            }

            return base.Deinitialize();
        }

        /// <summary>
        /// Add Native Elements.
        /// </summary>
        /// <param name="page">View.</param>
        internal void AddNativeElements(Microsoft.Maui.Controls.Page page)
        {
            if (this.panel == null)
            {
                return;
            }

            if (this.context == null)
            {
                return;
            }

            var element = page.ToNative(this.context);

            var zindex = 100 + this.Views.Count();
            if (element is null)
            {
                return;
            }

            element.SetValue(Canvas.ZIndexProperty, zindex);
            this.panel.Children.Add(element);
        }

        /// <summary>
        /// Remove Native Elements.
        /// </summary>
        /// <param name="page">Views.</param>
        internal void RemoveNativeElements(Microsoft.Maui.Controls.Page page)
        {
            if (this.panel == null)
            {
                return;
            }

            if (this.context == null)
            {
                return;
            }

            var element = page.ToNative(this.context);
            if (element is null)
            {
                return;
            }

            this.panel.Children.Remove(element);
        }

        private void Panel_PointerMoved(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (this.element == null || !this.HitTestElements.Any())
            {
                return;
            }

            var pointerPoint = e.GetCurrentPoint(this.element);
            if (pointerPoint == null)
            {
                return;
            }

            var nativeElements = this.HitTestElements.Select(n => n.GetNative(true));
            this.element.IsHitTestVisible = nativeElements.Any(n => n.GetBoundingBox().Contains(new Point(pointerPoint.Position.X, pointerPoint.Position.Y)));
        }
    }
}
