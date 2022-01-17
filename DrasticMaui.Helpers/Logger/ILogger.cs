// <copyright file="ILogger.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrasticMaui.Logger
{
    /// <summary>
    /// Logger.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log.
        /// </summary>
        /// <param name="message">Message to Log.</param>
        void Log(LogMessage message);
    }
}
