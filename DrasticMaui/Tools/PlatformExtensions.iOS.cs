using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace DrasticMaui.Tools
{
    /// <summary>
    /// iOS Platform Extensions.
    /// </summary>
    public static class PlatformExtensions
    {
        /// <summary>
        /// Get Native.
        /// </summary>
        /// <param name="view">IView.</param>
        /// <param name="returnWrappedIfPresent">Return wrapped if present.</param>
        /// <returns>UIView.</returns>
        public static UIView? GetNative(this IElement view, bool returnWrappedIfPresent)
        {
            if (view.Handler is INativeViewHandler nativeHandler && nativeHandler.NativeView != null)
            {
                return nativeHandler.NativeView;
            }

            return view.Handler?.NativeView as UIView;
        }

        /// <summary>
        /// Get View Transform.
        /// </summary>
        /// <param name="view">IView.</param>
        /// <returns>Matrix4x4.</returns>
        public static System.Numerics.Matrix4x4 GetViewTransform(this IView view)
        {
            var nativeView = view?.GetNative(true);
            if (nativeView == null)
            {
                return default(System.Numerics.Matrix4x4);
            }

            return nativeView.Layer.GetViewTransform();
        }

        /// <summary>
        /// Get View Transform.
        /// </summary>
        /// <param name="view">Native View.</param>
        /// <returns>Matrix4x4.</returns>
        public static System.Numerics.Matrix4x4 GetViewTransform(this UIView view)
            => view.Layer.GetViewTransform();

        /// <summary>
        /// Get Bounding Box.
        /// </summary>
        /// <param name="view">IView.</param>
        /// <returns>Rectangle.</returns>
        public static Microsoft.Maui.Graphics.Rectangle GetBoundingBox(this IView view)
            => view.GetNative(true).GetBoundingBox();

        /// <summary>
        /// Get Bounding Box.
        /// </summary>
        /// <param name="nativeView">Native View.</param>
        /// <returns>Rectangle.</returns>
        public static Microsoft.Maui.Graphics.Rectangle GetBoundingBox(this UIView? nativeView)
        {
            if (nativeView == null)
            {
                return default(Rectangle);
            }

            var nvb = nativeView.GetNativeViewBounds();
            var transform = nativeView.GetViewTransform();
            var radians = transform.ExtractAngleInRadians();
            var rotation = CoreGraphics.CGAffineTransform.MakeRotation((nfloat)radians);
            CGAffineTransform.CGRectApplyAffineTransform(nvb, rotation);
            return new Rectangle(nvb.X, nvb.Y, nvb.Width, nvb.Height);
        }

        /// <summary>
        /// Extract angle in radians.
        /// </summary>
        /// <param name="matrix">Matrix4x4.</param>
        /// <returns>Angle as double.</returns>
        public static double ExtractAngleInRadians(this System.Numerics.Matrix4x4 matrix)
            => Math.Atan2(matrix.M21, matrix.M11);

        /// <summary>
        /// Get Native View Bounds.
        /// </summary>
        /// <param name="view">IView.</param>
        /// <returns>Rectangle.</returns>
        public static Rectangle GetNativeViewBounds(this IView view)
        {
            var nativeView = view?.GetNative(true);
            if (nativeView == null)
            {
                return default(Rectangle);
            }

            return nativeView.GetNativeViewBounds();
        }

        /// <summary>
        /// Get Native View Bounds.
        /// </summary>
        /// <param name="nativeView">Native View.</param>
        /// <returns>Rectangle.</returns>
        public static Rectangle GetNativeViewBounds(this UIView nativeView)
        {
            if (nativeView == null)
            {
                return default(Rectangle);
            }

            var superview = nativeView;
            while (superview.Superview is not null)
            {
                superview = superview.Superview;
            }

            var convertPoint = nativeView.ConvertRectToView(nativeView.Bounds, superview);

            var x = convertPoint.X;
            var y = convertPoint.Y;
            var width = convertPoint.Width;
            var height = convertPoint.Height;

            return new Rectangle(x, y, width, height);
        }

        /// <summary>
        /// CATransform3D to view transform.
        /// </summary>
        /// <param name="transform">CATransform3D.</param>
        /// <returns>Matrix4x4.</returns>
        public static Matrix4x4 ToViewTransform(this CATransform3D transform) =>
           new Matrix4x4
           {
               M11 = (float)transform.m11,
               M12 = (float)transform.m12,
               M13 = (float)transform.m13,
               M14 = (float)transform.m14,
               M21 = (float)transform.m21,
               M22 = (float)transform.m22,
               M23 = (float)transform.m23,
               M24 = (float)transform.m24,
               M31 = (float)transform.m31,
               M32 = (float)transform.m32,
               M33 = (float)transform.m33,
               M34 = (float)transform.m34,
               Translation = new Vector3((float)transform.m41, (float)transform.m42, (float)transform.m43),
               M44 = (float)transform.m44,
           };

        /// <summary>
        /// Get View Transform.
        /// </summary>
        /// <param name="layer">Layer.</param>
        /// <returns>Matrix4x4.</returns>
        public static Matrix4x4 GetViewTransform(this CALayer layer)
        {
            if (layer == null)
            {
                return default(Matrix4x4);
            }

            var superLayer = layer.SuperLayer;
            if (layer.Transform.IsIdentity && (superLayer == null || superLayer.Transform.IsIdentity))
            {
                return default(Matrix4x4);
            }

            var superTransform = layer.SuperLayer?.GetChildTransform() ?? CATransform3D.Identity;

            return layer.GetLocalTransform()
                .Concat(superTransform)
                    .ToViewTransform();
        }

        /// <summary>
        /// Prepend.
        /// </summary>
        /// <param name="a">CATransform3D a.</param>
        /// <param name="b">CATransform3D b.</param>
        /// <returns>CATransform3D.</returns>
        public static CATransform3D Prepend(this CATransform3D a, CATransform3D b) =>
            b.Concat(a);

        /// <summary>
        /// Get Local Transform.
        /// </summary>
        /// <param name="layer">Layer.</param>
        /// <returns>CATransform3D.</returns>
        public static CATransform3D GetLocalTransform(this CALayer layer)
        {
            return CATransform3D.Identity
                .Translate(
                    layer.Position.X,
                    layer.Position.Y,
                    layer.ZPosition)
                .Prepend(layer.Transform)
                .Translate(
                    -layer.AnchorPoint.X * layer.Bounds.Width,
                    -layer.AnchorPoint.Y * layer.Bounds.Height,
                    -layer.AnchorPointZ);
        }

        /// <summary>
        /// Get Child Transform.
        /// </summary>
        /// <param name="layer">Layer.</param>
        /// <returns>CATransform3D.</returns>
        public static CATransform3D GetChildTransform(this CALayer layer)
        {
            var childTransform = layer.SublayerTransform;

            if (childTransform.IsIdentity)
            {
                return childTransform;
            }

            return CATransform3D.Identity
                .Translate(
                    layer.AnchorPoint.X * layer.Bounds.Width,
                    layer.AnchorPoint.Y * layer.Bounds.Height,
                    layer.AnchorPointZ)
                .Prepend(childTransform)
                .Translate(
                    -layer.AnchorPoint.X * layer.Bounds.Width,
                    -layer.AnchorPoint.Y * layer.Bounds.Height,
                    -layer.AnchorPointZ);
        }

        /// <summary>
        /// Transform to Ancestor.
        /// </summary>
        /// <param name="fromLayer">From Layer.</param>
        /// <param name="toLayer">To Layer.</param>
        /// <returns>CATransform3D.</returns>
        public static CATransform3D TransformToAncestor(this CALayer fromLayer, CALayer toLayer)
        {
            var transform = CATransform3D.Identity;

            CALayer? current = fromLayer;
            while (current != toLayer)
            {
                transform = transform.Concat(current.GetLocalTransform());

                current = current.SuperLayer;
                if (current == null)
                {
                    break;
                }

                transform = transform.Concat(current.GetChildTransform());
            }

            return transform;
        }

#if __MACCATALYST__
        /// <summary>
        /// Get NSWindow from UIWindow.
        /// </summary>
        /// <param name="window">UIWindow.</param>
        /// <returns>NSWindow as NSObject.</returns>
        public static async Task<NSObject?> GetNSWindowFromUIWindow(this UIWindow window)
        {
            if (window is null)
            {
                return null;
            }

            var nsApplication = Runtime.GetNSObject(Class.GetHandle("NSApplication"));
            if (nsApplication is null)
            {
                return null;
            }

            var sharedApplication = nsApplication.PerformSelector(new Selector("sharedApplication"));
            if (sharedApplication is null)
            {
                return null;
            }

            var applicationDelegate = sharedApplication.PerformSelector(new Selector("delegate"));
            if (applicationDelegate is null)
            {
                return null;
            }

            return await GetNSWindow(window, applicationDelegate);
        }

        public static async Task<NSObject?> GetNSWindow(UIWindow window, NSObject applicationDelegate)
        {
            var nsWindowHandle = IntPtr_objc_msgSend_IntPtr(applicationDelegate.Handle, Selector.GetHandle("hostWindowForUIWindow:"), window.Handle);
            var nsWindow = Runtime.GetNSObject<NSObject>(nsWindowHandle);
            if (nsWindow is null)
            {
                await Task.Delay(500);
                return await GetNSWindow(window, applicationDelegate);
            }

            return nsWindow;
        }

        public static async Task SetFrameForUIWindow(this UIWindow window, CGRect rect)
        {
            var nsWindow = await window.GetNSWindowFromUIWindow();
            if (nsWindow is null)
            {
                return;
            }

            var newRect = NSValue.FromCGRect(rect);

            void_objc_msgSend_IntPtr_bool(nsWindow.Handle, Selector.GetHandle("setFrame:display:"), newRect.Handle, false);
        }

        public static async Task ToggleTitleBarButtons(this UIWindow window, bool hideButtons)
        {
            var nsWindow = await window.GetNSWindowFromUIWindow();
            if (nsWindow is null)
            {
                return;
            }

            var closeButton = Runtime.GetNSObject(IntPtr_objc_msgSend_nfloat(nsWindow.Handle, Selector.GetHandle("standardWindowButton:"), 0));

            if (closeButton is null)
            {
                return;
            }

            var miniaturizeButton = Runtime.GetNSObject(IntPtr_objc_msgSend_nfloat(nsWindow.Handle, Selector.GetHandle("standardWindowButton:"), 1));
            if (miniaturizeButton is null)
            {
                return;
            }

            var zoomButton = Runtime.GetNSObject(IntPtr_objc_msgSend_nfloat(nsWindow.Handle, Selector.GetHandle("standardWindowButton:"), 2));

            if (zoomButton is null)
            {
                return;
            }

            void_objc_msgSend_bool(closeButton.Handle, Selector.GetHandle("isHidden"), hideButtons);
            void_objc_msgSend_bool(miniaturizeButton.Handle, Selector.GetHandle("isHidden"), hideButtons);
            void_objc_msgSend_bool(zoomButton.Handle, Selector.GetHandle("isHidden"), hideButtons);
        }

        public static void ToggleFullScreen(this UIWindow window, bool fullScreen)
        {
            var nsWindow = window.GetNSWindowFromUIWindow().Result;
            if (nsWindow is null)
            {
                return;
            }

            void_objc_msgSend_bool(nsWindow.Handle, Selector.GetHandle("toggleFullScreen:"), fullScreen);
        }

        public static NSObject? GetNSApplicationSharedApplication()
        {
            var nsapp = Runtime.GetNSObject(Class.GetHandle("NSApplication"));
            if (nsapp is null)
            {
                return null;
            }

            var sharedApp = nsapp.PerformSelector(new Selector("sharedApplication"));

            return null;
        }

        public static void NSApplicationActivateIgnoringOtherApps(bool ignoreSetting = true)
        {
            var sharedApplication = GetNSApplicationSharedApplication();
            if (sharedApplication is null)
            {
                return;
            }

            void_objc_msgSend_bool(sharedApplication.Handle, Selector.GetHandle("activateIgnoringOtherApps:"), ignoreSetting);
        }

        [DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
        internal static extern IntPtr IntPtr_objc_msgSend_nfloat(IntPtr receiver, IntPtr selector, nfloat arg1);

        [DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
        internal static extern IntPtr IntPtr_objc_msgSend_IntPtr(IntPtr receiver, IntPtr selector, IntPtr arg1);

        [DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
        internal static extern IntPtr IntPtr_objc_msgSend(IntPtr receiver, IntPtr selector);

        [DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
        internal static extern void void_objc_msgSend_IntPtr(IntPtr receiver, IntPtr selector, IntPtr arg1);

        [DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
        internal static extern void void_objc_msgSend_IntPtr_bool(IntPtr receiver, IntPtr selector, IntPtr arg1, bool arg2);

        [DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
        internal static extern void void_objc_msgSend_bool(IntPtr receiver, IntPtr selector, bool arg1);

        [DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
        internal static extern void void_objc_msgSend_ulong(IntPtr receiver, IntPtr selector, ulong arg1);
#endif
    }
}
