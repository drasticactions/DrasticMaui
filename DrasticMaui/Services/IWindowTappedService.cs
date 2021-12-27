// <copyright file="WindowTappedService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrasticMaui.Services
{
    /// <summary>
    /// Window Tapped Service.
    /// </summary>
    public interface IWindowTappedService
    {
        /// <summary>
        /// Fired when hidden.
        /// </summary>
        public event EventHandler OnHidden;

        /// <summary>
        /// Fired when Tapped.
        /// </summary>
        public event EventHandler OnTapped;

        /// <summary>
        /// Starts Service.
        /// </summary>
        public void StartService();

        /// <summary>
        /// Ends Service.
        /// </summary>
        public void StopService();
    }
}

