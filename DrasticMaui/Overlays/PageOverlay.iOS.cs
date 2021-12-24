// <copyright file="PageOverlay.iOS.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using CoreGraphics;
using DrasticMaui.Tools;
using Microsoft.Maui.Platform;
using ObjCRuntime;
using UIKit;

namespace DrasticMaui.Overlays
{
    /// <summary>
    /// Page Overlay.
    /// </summary>
    public partial class PageOverlay
    {
        private PassthroughView? passthroughView;
        private UIWindow? window;

        /// <inheritdoc/>
        public override bool Initialize()
        {
            if (this.pageOverlayNativeElementsInitialized)
            {
                return true;
            }

            base.Initialize();

            var handler = this.Window?.Handler;
            if (handler == null)
            {
                return false;
            }

            this.context = handler.MauiContext;

            var nativeLayer = this.Window?.GetNative(true);
            if (nativeLayer is not UIWindow nativeWindow)
            {
                return false;
            }

            this.window = nativeWindow;

            if (nativeWindow?.RootViewController?.View == null)
            {
                return false;
            }

            this.passthroughView = new PassthroughView(this, nativeWindow.RootViewController.View.Frame);
            nativeWindow.RootViewController.View.AddSubview(this.passthroughView);
            nativeWindow.RootViewController.View.BringSubviewToFront(this.passthroughView);
            return this.pageOverlayNativeElementsInitialized = true;
        }

        /// <summary>
        /// Add Native Elements.
        /// </summary>
        /// <param name="page">View.</param>
        internal void AddNativeElements(Page page)
        {
            if (this.context is null)
            {
                return;
            }

            var element = page.ToHandler(this.context);
            if (element.NativeView is null || this.passthroughView is null || this.window?.RootViewController?.View is null)
            {
                return;
            }

            element.NativeView.Frame = this.passthroughView.Frame;
            element.NativeView.AutoresizingMask = UIViewAutoresizing.All;
            this.passthroughView.AddSubview(element.NativeView);
            this.passthroughView.BringSubviewToFront(element.NativeView);
            this.passthroughView.AutoresizingMask = UIViewAutoresizing.All;
            this.window?.RootViewController.View.BringSubviewToFront(this.passthroughView);
        }

        /// <summary>
        /// Remove Native Elements.
        /// </summary>
        /// <param name="page">Views.</param>
        internal void RemoveNativeElements(Page page)
        {
            if (this.context is null)
            {
                return;
            }

            var element = page.ToHandler(this.context);
            if (element.NativeView is null || this.passthroughView is null || this.window?.RootViewController?.View is null)
            {
                return;
            }

            element.NativeView.RemoveFromSuperview();
        }

        private class PassthroughView : UIView
        {
            private PageOverlay overlay;

            /// <summary>
            /// Initializes a new instance of the <see cref="PassthroughView"/> class.
            /// </summary>
            /// <param name="overlay">The Window Overlay.</param>
            /// <param name="frame">Base Frame.</param>
            public PassthroughView(PageOverlay windowOverlay, CGRect frame)
                : base(frame)
            {
                this.overlay = windowOverlay;
            }

            public override bool PointInside(CGPoint point, UIEvent? uievent)
            {
                foreach (var element in this.overlay.HitTestElements)
                {
                    var boundingBox = element.GetBoundingBox();
                    if (boundingBox.Contains(point.X, point.Y))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
