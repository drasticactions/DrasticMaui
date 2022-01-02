// <copyright file="TaggedLogger.cs" company="Drastic Actions">
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
    /// Tagged Logger.
    /// </summary>
    public class TaggedLogger : ILogger
    {
        private ILogger logger;
        private string tag;
        private bool includeTagInUserVisibileMessages;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaggedLogger"/> class.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="tag">Tag.</param>
        /// <param name="includeTagInUserVisibileMessages">Include Tag In User Visibile Messages.</param>
        public TaggedLogger(ILogger logger, string tag, bool includeTagInUserVisibileMessages)
        {
            this.logger = logger;
            this.tag = tag;
            this.includeTagInUserVisibileMessages = includeTagInUserVisibileMessages;
        }

        /// <summary>
        /// Log.
        /// </summary>
        /// <param name="log">Log Message.</param>
        public void Log(LogMessage log)
        {
            LogLevel level = log.Level;
            bool isUserVisible = level == LogLevel.Info || level == LogLevel.Warn || level == LogLevel.Error;
            bool includeTag = !isUserVisible || this.includeTagInUserVisibileMessages;

            if (includeTag)
            {
                this.logger.Log(log.WithMessage($"({tag}) {log.Message}"));
            }
            else
            {
                this.logger.Log(log);
            }
        }
    }
}
