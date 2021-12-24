// <copyright file="PlatformExtensions.Android.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Numerics;
using System.Threading.Tasks;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using AndroidX.Core.View;
using Microsoft.Maui.Essentials;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using ALayoutDirection = Android.Views.LayoutDirection;
using ATextDirection = Android.Views.TextDirection;
using AView = Android.Views.View;
using GL = Android.Opengl;

namespace DrasticMaui.Tools
{
    /// <summary>
    /// Android Platform Extensions.
    /// </summary>
    public static class PlatformExtensions
    {
        /// <summary>
        /// Get native element..
        /// </summary>
        /// <param name="view">IElement.</param>
        /// <param name="returnWrappedIfPresent">Return wrapped if present.</param>
        /// <returns>AView.</returns>
        public static AView? GetNative(this IElement view, bool returnWrappedIfPresent)
        {
            if (view.Handler is INativeViewHandler nativeHandler && nativeHandler.NativeView != null)
            {
                return nativeHandler.NativeView;
            }

            return view.Handler?.NativeView as AView;
        }

        /// <summary>
        /// Gets Navigation Root Manager.
        /// </summary>
        /// <param name="mauiContext">MAUI Context.</param>
        /// <returns>NavigationRootManager.</returns>
        public static NavigationRootManager GetNavigationRootManager(this IMauiContext mauiContext) =>
            mauiContext.Services.GetRequiredService<NavigationRootManager>();

        /// <summary>
        /// Get Children View.
        /// </summary>
        /// <typeparam name="T">Base View Type.</typeparam>
        /// <param name="view">View.</param>
        /// <returns>List of T.</returns>
        public static List<T> GetChildView<T>(this Android.Views.ViewGroup view)
        {
            var childCount = view.ChildCount;
            var list = new List<T>();
            for (var i = 0; i < childCount; i++)
            {
                var child = view.GetChildAt(i);
                if (child is T tChild)
                {
                    list.Add(tChild);
                }
            }

            return list;
        }

        /// <summary>
        /// Get the bounding box for an Android View.
        /// </summary>
        /// <param name="view">MAUI IView.</param>
        /// <returns>Rectangle.</returns>
        public static Microsoft.Maui.Graphics.Rectangle GetBoundingBox(this IView view)
            => view.GetNative(true).GetBoundingBox();

        /// <summary>
        /// Get the bounding box for an Android View.
        /// </summary>
        /// <param name="nativeView">Android View.</param>
        /// <returns>Rectangle.</returns>
        public static Microsoft.Maui.Graphics.Rectangle GetBoundingBox(this Android.Views.View? nativeView)
        {
            if (nativeView == null)
            {
                return default(Rectangle);
            }

            var rect = new Android.Graphics.Rect();
            nativeView.GetGlobalVisibleRect(rect);
            return new Rectangle(rect.ExactCenterX() - (rect.Width() / 2), rect.ExactCenterY() - (rect.Height() / 2), (float)rect.Width(), (float)rect.Height());
        }
    }
}
