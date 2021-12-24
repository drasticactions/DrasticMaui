// <copyright file="DragAndDropOverlay.Android.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using AndroidX.CoordinatorLayout.Widget;
using DrasticMaui.Tools;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Native;
using Microsoft.Maui.Handlers;

namespace DrasticMaui.Overlays
{
    /// <summary>
    /// Drag and Drop Overlay.
    /// </summary>
    public partial class DragAndDropOverlay
    {
        private ViewGroup? nativeLayer;
        private DragAndDropView? dragAndDropView;

        /// <inheritdoc/>
        public override bool Initialize()
        {
            if (this.dragAndDropOverlayNativeElementsInitialized)
            {
                return true;
            }

            base.Initialize();

            var nativeWindow = this.Window?.Content?.GetNative(true);
            if (nativeWindow is null)
            {
                return false;
            }

            var handler = this.Window?.Handler as WindowHandler;
            if (handler?.MauiContext is null)
            {
                return false;
            }

            var rootManager = handler.MauiContext.GetNavigationRootManager();
            if (rootManager is null)
            {
                return false;
            }

            if (handler.NativeView is not Activity activity)
            {
                return false;
            }

            var context = activity.ApplicationContext;
            if (context is null)
            {
                return false;
            }

            this.nativeLayer = rootManager.RootView as ViewGroup;

            if (this.nativeLayer is null)
            {
                return false;
            }

            this.dragAndDropView = new DragAndDropView(context);
            this.nativeLayer.AddView(this.dragAndDropView, 0, new CoordinatorLayout.LayoutParams(CoordinatorLayout.LayoutParams.MatchParent, CoordinatorLayout.LayoutParams.MatchParent));
            this.dragAndDropView.BringToFront();
            return this.dragAndDropOverlayNativeElementsInitialized = true;
        }

        private class DragAndDropView : Android.Views.View, IOnReceiveContentListener
        {
            public DragAndDropView(Context context)
                : base(context)
            {
            }

            protected DragAndDropView(IntPtr javaReference, JniHandleOwnership transfer)
                : base(javaReference, transfer)
            {
            }

            public ContentInfo? OnReceiveContent(Android.Views.View view, ContentInfo payload)
            {
                return null;
            }
        }
    }
}
