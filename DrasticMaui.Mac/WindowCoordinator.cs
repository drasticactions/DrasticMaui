// <copyright file="WindowCoordinator.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using AppKit;

namespace DrasticMaui.Mac
{
    public class WindowCoordinator : ITrayService
    {
        public void Initialize()
        {
            NSStatusBar statusBar = NSStatusBar.SystemStatusBar;
            var statusBarItem = statusBar.CreateStatusItem(NSStatusItemLength.Variable);
            if (statusBarItem is null)
            {
                throw new NullReferenceException(nameof(statusBarItem));
            }
        }
    }
}