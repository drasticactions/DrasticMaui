// <copyright file="PageOverlay.Android.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrasticMaui.Tools;
using Android.App;
using Android.Views;
using AndroidX.CoordinatorLayout.Widget;
using Microsoft.Maui.Handlers;

namespace DrasticMaui.Overlays
{
    /// <summary>
    /// Page Overlay.
    /// </summary>
    public partial class PageOverlay
    {
        private Activity? nativeActivity;
        private ViewGroup? nativeLayer;

        /// <inheritdoc/>
        public override bool Initialize()
        {
            if (this.pageOverlayNativeElementsInitialized)
            {
                return true;
            }

            if (Window == null)
            {
                return false;
            }

            var nativeWindow = Window?.Content?.GetNative(true);
            if (nativeWindow == null)
            {
                return false;
            }

            var handler = Window?.Handler as WindowHandler;
            if (handler?.MauiContext == null)
            {
                return false;
            }

            this.context = handler.MauiContext;

            var rootManager = handler.MauiContext.GetNavigationRootManager();
            if (rootManager == null)
            {
                return false;
            }


            if (handler.NativeView is not Activity activity)
            {
                return false;
            }

            nativeActivity = activity;
            nativeLayer = rootManager.RootView as ViewGroup;

            if (nativeLayer?.Context == null)
            {
                return false;
            }

            if (nativeActivity?.WindowManager?.DefaultDisplay == null)
            {
                return false;
            }

            return this.pageOverlayNativeElementsInitialized = true;
        }

        /// <inheritdoc/>
        public override bool Deinitialize()
        {
            this.RemoveViews();
            return base.Deinitialize();
        }

        /// <summary>
        /// Add Native Elements.
        /// </summary>
        /// <param name="view">View.</param>
        internal void AddNativeElements(Page view)
        {
            if (this.nativeLayer == null || this.context == null)
            {
                return;
            }

            var pageHandler = view.ToHandler(this.context);
            var element = pageHandler?.NativeView;
            if (element is not null)
            {
                element.Touch += this.Element_Touch;
                var layerCount = nativeLayer.ChildCount;
                var childView = nativeLayer.GetChildAt(1);
                nativeLayer.AddView(element, layerCount, new CoordinatorLayout.LayoutParams(CoordinatorLayout.LayoutParams.MatchParent, CoordinatorLayout.LayoutParams.MatchParent));
                element.BringToFront();
            }
        }

        /// <summary>
        /// Remove Native Elements.
        /// </summary>
        /// <param name="view">Views.</param>
        internal void RemoveNativeElements(Page view)
        {
            if (this.nativeLayer == null || this.context is null)
            {
                return;
            }

            var pageHandler = view.ToHandler(this.context);
            var element = pageHandler?.NativeView;
            if (element is not null)
            {
                element.Touch -= this.Element_Touch;
                this.nativeLayer.RemoveView(element);
            }
        }

        private void Element_Touch(object? sender, Android.Views.View.TouchEventArgs e)
        {
            if (e?.Event == null)
            {
                return;
            }

            if (e.Event.Action != MotionEventActions.Down && e.Event.ButtonState != MotionEventButtonState.Primary)
            {
                return;
            }

            var point = new Point(e.Event.RawX, e.Event.RawY);
            e.Handled = this.HitTestElements.Any(n => n.GetBoundingBox().Contains(point));
        }
    }
}
