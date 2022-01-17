using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrasticMaui.Events;
using DrasticMaui.Logger;

namespace DrasticMaui.Services
{
    /// <summary>
    /// Error Handler Service.
    /// </summary>
    public class ErrorHandlerService : IErrorHandlerService
    {
        private IEnumerable<ILogger> loggers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlerService"/> class.
        /// </summary>
        /// <param name="loggers">Loggers.</param>
        public ErrorHandlerService(IEnumerable<ILogger>? loggers)
        {
            this.loggers = loggers ?? new List<ILogger>();
        }

        /// <inheritdoc/>
        public event EventHandler<DrasticErrorHandlerEventArgs>? OnError;

        /// <inheritdoc/>
        public void HandleError(Exception exception)
        {
            if (exception == null)
            {
                return;
            }

            // TODO: Log exception to error handling service provider.
            string errorMessage = string.Format("Error", exception.GetType().FullName, exception.Message, exception.StackTrace);

            foreach (var logger in this.loggers)
            {
                logger.Log(LogLevel.Error, errorMessage);
            }

            this.OnError?.Invoke(this, new DrasticErrorHandlerEventArgs(exception));
        }
    }
}
