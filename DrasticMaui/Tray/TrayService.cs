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
        private IMauiContext context;
        private DrasticTrayWindow? trayWindow;

        public TrayService(string name, Stream stream, IMauiContext context)
        {
            this.iconName = name;
            this.iconStream = stream;
            this.context = context;
        }

#if ANDROID || IOS

        public void SetupPage (Microsoft.Maui.Controls.Page page)
        {
        
        }

		public void SetupTrayIcon()
        {

        }
#endif
    }
}