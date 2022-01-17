using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrasticMaui.Logger
{
    /// <summary>
    /// Log Message.
    /// </summary>
    [Serializable]
    public sealed class LogMessage
    {
        /// <summary>
        /// Timestamp Format.
        /// </summary>
        public const string TimestampFormat = "yyyy-MM-dd HH:mm:ss.f";

        /// <summary>
        /// Initializes a new instance of the <see cref="LogMessage"/> class.
        /// </summary>
        /// <param name="timestamp">Datetime Stamp.</param>
        /// <param name="level">Log Level.</param>
        /// <param name="message">Message.</param>
        public LogMessage(DateTime timestamp, LogLevel level, string message)
        {
            if (level <= LogLevel.All || level >= LogLevel.None)
            {
                throw new ArgumentException("Invalid log level", nameof(level));
            }

            if (message is null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            this.Timestamp = timestamp;
            this.Level = level;
            this.Message = message;
        }

        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Gets the level.
        /// </summary>
        public LogLevel Level { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Log with Message.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <returns>LogMessage.</returns>
        public LogMessage WithMessage(string message)
        {
            var result = (LogMessage)this.MemberwiseClone();
            result.Message = message;
            return result;
        }

        /// <inheritdoc/>
        public override string ToString()
            => $"[DrasticMaui] ({this.Timestamp.ToString(TimestampFormat)}): {this.Level.ToString().ToUpperInvariant()}: {this.Message}";

        /// <summary>
        /// Return message appropriate for the output pad/pane, which is more visible to the end user (but still technical).
        /// Here, we want to have somewhat concise output, minimzing horizontal scrolling.
        /// </summary>
        /// <returns>string.</returns>
        public string ToOutputPaneString()
        {
            // In the output pane, only show time, not date, to make it more concise.
            // Use the locale specific long time format (e.g. "1:45:30 PM" for en-US)
            var timestamp = this.Timestamp.ToString("T");

            return $"[{timestamp}]  {this.Message}";
        }
    }
}
