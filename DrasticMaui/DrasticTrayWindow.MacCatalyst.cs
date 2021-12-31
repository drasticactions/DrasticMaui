// <copyright file="DrasticTrayWindow.MacCatalyst.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrasticMaui
{
    /// <summary>
    /// Drastic Tray Window.
    /// </summary>
    public partial class DrasticTrayWindow
    {
        protected override void OnCreated()
        {
            base.OnCreated();
            this.SetupWindow();
        }

        private void SetupWindow()
        {
        }
    }
}