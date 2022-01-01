using System;
using CoreGraphics;
using DrasticMaui.Tools;
using Foundation;
using Microsoft.Maui.Handlers;
using ObjCRuntime;
using UIKit;

namespace DrasticMaui
{
    public partial class DrasticTrayIcon : NSObject
    {
        private AppKit.NSImage? statusBarImage;
        private NSObject? systemStatusBarObj;
        private NSObject? statusBarObj;
        private NSObject? statusBarItem;
        private NSObject? statusBarButton;

        private void SetupStatusBarButton()
        {
            this.statusBarObj = Runtime.GetNSObject(Class.GetHandle("NSStatusBar"));
            if (this.statusBarObj is null)
            {
                return;
            }

            this.systemStatusBarObj = this.statusBarObj.PerformSelector(new Selector("systemStatusBar"));
            this.statusBarItem = Runtime.GetNSObject(PlatformExtensions.IntPtr_objc_msgSend_nfloat(this.systemStatusBarObj.Handle, Selector.GetHandle("statusItemWithLength:"), -1));
            if (this.statusBarItem is null)
            {
                return;
            }

            this.statusBarButton = Runtime.GetNSObject(PlatformExtensions.IntPtr_objc_msgSend(this.statusBarItem.Handle, Selector.GetHandle("button")));

            if (this.statusBarButton is not null && this.statusBarImage is not null)
            {
                PlatformExtensions.void_objc_msgSend_IntPtr(this.statusBarButton.Handle, Selector.GetHandle("setImage:"), this.statusBarImage.Handle);
                PlatformExtensions.void_objc_msgSend_bool(this.statusBarImage.Handle, Selector.GetHandle("setTemplate:"), true);
                this.statusBarImage.Size = new CoreGraphics.CGSize(32, 32);
            }

            if (this.statusBarButton is not null)
            {
                // Handle click
                PlatformExtensions.void_objc_msgSend_IntPtr(this.statusBarButton.Handle, Selector.GetHandle("setTarget:"), this.Handle);
                PlatformExtensions.void_objc_msgSend_IntPtr(this.statusBarButton.Handle, Selector.GetHandle("setAction:"), new Selector("handleButtonClick:").Handle);
            }
        }

        private void SetupStatusBarImage()
        {
            if (this.iconStream is null)
            {
                return;
            }

            var imageStream = Foundation.NSData.FromStream(this.iconStream);
            this.statusBarImage = Runtime.GetNSObject<AppKit.NSImage>(PlatformExtensions.IntPtr_objc_msgSend(ObjCRuntime.Class.GetHandle("NSImage"), Selector.GetHandle("alloc")));
            if (this.statusBarImage is null || imageStream is null)
            {
                throw new ArgumentNullException(nameof(this.statusBarImage));
            }

            PlatformExtensions.IntPtr_objc_msgSend_IntPtr(this.statusBarImage.Handle, Selector.GetHandle("initWithData:"), imageStream.Handle);
        }

        [Export("handleButtonClick:")]
        private void HandleClick(NSObject senderStatusBarButton)
        {
            PlatformExtensions.NSApplicationActivateIgnoringOtherApps(true);
            this.LeftClicked?.Invoke(this, EventArgs.Empty);
        }

        private void SetupStatusBarMenu()
        {
        }

        private void NativeElementDispose()
        {
        }
    }
}