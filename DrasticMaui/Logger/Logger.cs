// <copyright file="Logger.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DrasticMaui.Logger
{
    /// <summary>
    /// Logger.
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// Log.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="level">Log Level.</param>
        /// <param name="message">Message.</param>
        public static void Log(this ILogger logger, LogLevel level, string? message)
            => logger.Log(new LogMessage(DateTime.Now, level, message ?? string.Empty));

        /// <summary>
        /// Log Error.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="ex">Exception.</param>
        /// <param name="level">Level.</param>
        /// <param name="memberName">Member Name.</param>
        /// <param name="sourceLineNumber">Source Line Number.</param>
        public static void Log(
            this ILogger logger,
            Exception ex,
            LogLevel level = LogLevel.Error,
            [CallerMemberName] string memberName = "(unknown)",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            logger.Log(level, $"Caught exception in {memberName} at {sourceLineNumber}: {ex}\n{ex.StackTrace}");
        }

        /// <summary>
        /// Log if task is faulted.
        /// </summary>
        /// <param name="task">Task.</param>
        /// <param name="logger">Logger.</param>
        /// <param name="level">Level.</param>
        /// <param name="memberName">Source Name.</param>
        /// <param name="sourceLineNumber">Source Line Number.</param>
        public static void LogIfFaulted(
            this Task task,
            ILogger logger,
            LogLevel level = LogLevel.Error,
            [CallerMemberName] string memberName = "(unknown)",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            task.ContinueWith(
                t =>
            {
                if (t.Exception is null)
                {
                    return;
                }

                logger.Log(t.Exception, level, memberName, sourceLineNumber);
            },
                TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
        }

        /// <summary>
        /// Log for perf..
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="sw">Stop Watch.</param>
        /// <param name="level">Log Level.</param>
        /// <param name="memberName">Member Name.</param>
        /// <param name="sourceLineNumber">Source Line Number.</param>
        public static void Log(
            this ILogger logger,
            Stopwatch sw,
            LogLevel level = LogLevel.Perf,
            [CallerMemberName] string memberName = "(unknown)",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            logger.Log(level, $"Elapsed time in {memberName} at {sourceLineNumber}: {sw.ElapsedMilliseconds}ms");
        }

        /// <summary>
        /// Returns a new <see cref="ILogger"/> that prefixes every message with
        /// parenthesis and the given tag.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="tag">Tag.</param>
        /// <param name="includeTagInUserVisibleMessages">Include Tag in User Visible Messages.</param>
        /// <returns>New Logger.</returns>
        public static ILogger WithTag(this ILogger logger, string tag, bool includeTagInUserVisibleMessages = false)
            => new TaggedLogger(logger, tag, includeTagInUserVisibleMessages);
    }
}
