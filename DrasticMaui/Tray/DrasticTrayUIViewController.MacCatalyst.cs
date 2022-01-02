using System;
using CoreGraphics;
using DrasticMaui.Options;
using DrasticMaui.Tools;
using Foundation;
using UIKit;

namespace DrasticMaui
{
    public class DrasticTrayUIViewController : UIViewController
    {
        private UIViewController contentController;
        private UIImage? image;
        private UIWindow window;
        private DrasticTrayWindowOptions options;
        private DrasticTrayIcon trayIcon;

        public DrasticTrayUIViewController(
            UIWindow window,
            UIViewController contentController,
            DrasticTrayIcon trayIcon,
            DrasticTrayWindowOptions options)
        {
            this.trayIcon = trayIcon;
            this.options = options;
            this.window = window;
            this.contentController = contentController;
            this.image = UIImage.GetSystemImage("cursorarrow.click.2");
            this.SetupWindow();
        }

        public async void ToggleVisibility()
        {
            if (this.contentController?.View is null)
            {
                return;
            }

            if (this.View is null)
            {
                return;
            }

            var buttonBounds = this.trayIcon.GetFrame();
            await this.window.SetFrameForUIWindow(buttonBounds);
            var viewController = this.contentController;

            if (viewController.PresentingViewController is not null)
            {
                viewController.DismissViewController(true, null);
            }
           else
            {
                viewController.ModalPresentationStyle = UIModalPresentationStyle.Popover;
                viewController.PopoverPresentationController.SourceView = this.View;
                viewController.PopoverPresentationController.SourceRect = new CGRect(0, 0, 1, 1);
                viewController.PopoverPresentationController.PermittedArrowDirections = UIPopoverArrowDirection.Up;
                this.PresentViewController(viewController, true, null);
            }
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            this.PrepareForAppearance();
            this.ForceContentViewLayout();
        }

        private async void PrepareForAppearance()
        {
            if (this.window is not null)
            {
                await this.window.ToggleTitleBarButtons(true);
            }
        }

        private void ForceContentViewLayout()
        {
            if (this.contentController.View is not null)
            {
                this.View?.AddSubview(this.contentController.View);
                this.contentController.View.Frame = new CoreGraphics.CGRect(0, 0, this.options.WindowWidth, this.options.WindowHeight);
                this.View?.LayoutIfNeeded();
                this.contentController.View.RemoveFromSuperview();
            }
        }

        private void SetupWindow()
        {
            this.window.RootViewController = this;
            if (this.window.WindowScene?.Titlebar is null)
            {
                return;
            }

            if (this.window.WindowScene?.SizeRestrictions is null)
            {
                return;
            }

            this.window.WindowScene.Titlebar.TitleVisibility = UITitlebarTitleVisibility.Hidden;
            this.window.WindowScene.SizeRestrictions.MinimumSize = new CoreGraphics.CGSize(1, 1);
            this.window.WindowScene.SizeRestrictions.MaximumSize = new CoreGraphics.CGSize(1, 1);
        }
    }
}