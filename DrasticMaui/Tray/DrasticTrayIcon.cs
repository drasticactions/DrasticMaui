// <copyright file="DrasticTrayIcon.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;

namespace DrasticMaui
{
    public partial class DrasticTrayIcon
    {
        private Stream? iconStream;
        private string? iconName;

        public DrasticTrayIcon(string name, Stream stream)
        {
            this.iconName = name;
            this.iconStream = stream;
            this.SetupStatusBarImage();
            this.SetupStatusBarButton();
        }

        public event EventHandler<EventArgs>? Clicked;

#if !__MACCATALYST__ && !WINDOWS
        private void SetupStatusBarButton()
        {
        }

        private void SetupStatusBarImage()
        {
        }
#endif
    }
}