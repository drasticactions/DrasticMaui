// <copyright file="TrayService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
namespace DrasticMaui.Tray
{
	public partial class TrayService
	{
#if ANDROID || IOS
		public void SetupTrayIcon()
        {

        }
#endif
	}
}

