using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrasticMaui.Logger
{
    /// <summary>
    /// Log Level.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Used for filtering only; All messages are logged.
        /// </summary>
        All, // must be first

        /// <summary>
        /// Informational messages used for debugging or to trace code execution.
        /// </summary>
        Debug,

        /// <summary>
        /// Informational messages containing performance metrics.
        /// </summary>
        Perf,

        /// <summary>
        /// Informational messages that might be of interest to the user.
        /// </summary>
        Info,

        /// <summary>
        /// Warnings.
        /// </summary>
        Warn,

        /// <summary>
        /// Errors that are handled gracefully.
        /// </summary>
        Error,

        /// <summary>
        /// Errors that are not handled gracefully.
        /// </summary>
        Fail,

        /// <summary>
        /// Used for filtering only; No messages are logged.
        /// </summary>
        None, // must be last
    }
}
