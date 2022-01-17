// <copyright file="DrasticErrorHandlerEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DrasticMaui.Events
{
    /// <summary>
    /// Drastic Error Handler Event Args.
    /// </summary>
    public class DrasticErrorHandlerEventArgs : EventArgs
    {
        private readonly Exception exception;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrasticErrorHandlerEventArgs"/> class.
        /// </summary>
        /// <param name="ex">Exception.</param>
        public DrasticErrorHandlerEventArgs(Exception ex)
        {
            this.exception = ex;
        }

        /// <summary>
        /// Gets the Drastic Sliders's current position.
        /// </summary>
        public Exception Exception => this.exception;
    }
}
