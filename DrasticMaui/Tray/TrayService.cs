// <copyright file="TrayService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;

namespace DrasticMaui.Tray
{
    public partial class TrayService
    {
        private Stream iconStream;
        private string iconName;

        public TrayService(string name, Stream stream)
        {
            this.iconName = name;
            this.iconStream = stream;
        }

#if ANDROID || IOS
		public void SetupTrayIcon()
        {

        }
#endif
    }
}