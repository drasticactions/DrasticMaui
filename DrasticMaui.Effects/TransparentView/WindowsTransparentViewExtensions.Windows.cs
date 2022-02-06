// <copyright file="WindowsTransparentViewExtensions.Windows.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.Maui.Platform;

namespace DrasticMaui.Effects
{
    public static class WindowsTransparentViewExtensions
    {
        public static void ApplyAcrylic(this IView view, IMauiContext context)
        {
            var handler = view.ToHandler(context);
            if (handler is null)
            {
                return;
            }

            var platform = handler.HasContainer ? handler.ContainerView : handler.NativeView;
            var layoutPanel = platform as LayoutPanel;
            if (layoutPanel is null)
            {
                return;
            }

            layoutPanel.ApplyAcrlyic();
        }

        public static void ApplyAcrlyic(this Microsoft.UI.Xaml.Controls.Panel panel)
        {
            panel.Background = MauiWinUIApplication.Current.Resources["SystemControlAcrylicElementBrush"] as Microsoft.UI.Xaml.Media.Brush;
        }

        public static void ApplyAcrlyic(this Microsoft.UI.Xaml.Window window)
        {
            if (window.Content is Microsoft.UI.Xaml.Controls.Panel panel)
            {
                panel.ApplyAcrlyic();
            }
        }

        public static void RemoveAcrylic(this Microsoft.UI.Xaml.Controls.Panel panel)
        {
            panel.Background = null;
        }

        public static void RemoveAcrylic(this Microsoft.UI.Xaml.Window window)
        {
            if (window.Content is Microsoft.UI.Xaml.Controls.Panel panel)
            {
                panel.RemoveAcrylic();
            }
        }
    }
}
