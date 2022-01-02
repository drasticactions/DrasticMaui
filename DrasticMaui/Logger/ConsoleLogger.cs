// <copyright file="ConsoleLogger.cs" company="Drastic Actions">
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
    /// Console Logger.
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        /// <summary>
        /// Gets or Sets the minimum <see cref="LogLevel"/> for this logger.
        /// </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.All;

        /// <inheritdoc/>
        public virtual void Log(LogMessage message)
        {
            if (message.Level < this.LogLevel)
            {
                return;
            }

            Console.WriteLine(message.Message);
        }
    }
}
