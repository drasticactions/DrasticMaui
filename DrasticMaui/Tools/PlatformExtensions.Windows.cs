// <copyright file="PlatformExtensions.Windows.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using Microsoft.UI.Xaml;
using WinPoint = Windows.Foundation.Point;

namespace DrasticMaui.Tools
{
    /// <summary>
    /// Windows Platform Extensions.
    /// </summary>
    public static class PlatformExtensions
    {
        /// <summary>
        /// Get Native View.
        /// </summary>
        /// <param name="view">IElement.</param>
        /// <param name="returnWrappedIfPresent">Return wrapped if present.</param>
        /// <returns>FrameworkElement.</returns>
        public static FrameworkElement? GetNative(this IElement view, bool returnWrappedIfPresent)
        {
            if (view.Handler is INativeViewHandler nativeHandler && nativeHandler.NativeView != null)
            {
                return nativeHandler.NativeView;
            }

            return view.Handler?.NativeView as FrameworkElement;
        }

        internal static Rectangle GetBoundingBox(this IView view)
            => view.GetNative(true).GetBoundingBox();

        internal static Rectangle GetBoundingBox(this FrameworkElement? nativeView)
        {
            if (nativeView == null)
                return new Rectangle();

            var rootView = nativeView.XamlRoot.Content;
            if (nativeView == rootView)
            {
                if (rootView is not FrameworkElement el)
                    return new Rectangle();

                return new Rectangle(0, 0, el.ActualWidth, el.ActualHeight);
            }

            var topLeft = nativeView.TransformToVisual(rootView).TransformPoint(new WinPoint());
            var topRight = nativeView.TransformToVisual(rootView).TransformPoint(new WinPoint(nativeView.ActualWidth, 0));
            var bottomLeft = nativeView.TransformToVisual(rootView).TransformPoint(new WinPoint(0, nativeView.ActualHeight));
            var bottomRight = nativeView.TransformToVisual(rootView).TransformPoint(new WinPoint(nativeView.ActualWidth, nativeView.ActualHeight));

            var x1 = new[] { topLeft.X, topRight.X, bottomLeft.X, bottomRight.X }.Min();
            var x2 = new[] { topLeft.X, topRight.X, bottomLeft.X, bottomRight.X }.Max();
            var y1 = new[] { topLeft.Y, topRight.Y, bottomLeft.Y, bottomRight.Y }.Min();
            var y2 = new[] { topLeft.Y, topRight.Y, bottomLeft.Y, bottomRight.Y }.Max();
            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }
    }
}
