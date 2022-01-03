// <copyright file="DrasticMauiSampleTrayWindow.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrasticMaui.Options;

namespace DrasticMaui.Sample
{
    /// <summary>
    /// Drastic Maui Sample Tray Window.
    /// </summary>
    public class DrasticMauiSampleTrayWindow : DrasticTrayWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrasticMauiSampleTrayWindow"/> class.
        /// </summary>
        /// <param name="services">Service Provider.</param>
        /// <param name="icon">Tray Icon.</param>
        /// <param name="options">Options.</param>
        public DrasticMauiSampleTrayWindow(IServiceProvider services, DrasticTrayIcon icon, DrasticTrayWindowOptions? options = null)
            : base(services, icon, options)
        {
        }
    }
}
