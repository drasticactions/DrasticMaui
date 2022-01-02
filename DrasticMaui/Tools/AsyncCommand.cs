// <copyright file="AsyncCommand.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using System.Windows.Input;
using DrasticMaui.Services;
using Microsoft.Maui.Essentials;

namespace DrasticMaui.Tools
{
    /// <summary>
    /// Async Command.
    /// </summary>
    public class AsyncCommand : IAsyncCommand
    {
        private readonly Func<Task>? execute;
        private readonly Func<bool>? canExecute;
        private readonly IErrorHandlerService? errorHandler;
        private bool isExecuting;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncCommand"/> class.
        /// </summary>
        /// <param name="execute">Command to execute.</param>
        /// <param name="canExecute">Can execute command.</param>
        /// <param name="errorHandler">Error handler.</param>
        public AsyncCommand(
            Func<Task> execute,
            Func<bool>? canExecute = null,
            IErrorHandlerService? errorHandler = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
            this.errorHandler = errorHandler;
        }

        /// <summary>
        /// Can Execute Changed.
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Gets or sets a value indicating whether the command is executing.
        /// </summary>
        protected bool IsExecuting
        {
            get
            {
                return this.isExecuting;
            }

            set
            {
                this.isExecuting = value;
                this.RaiseCanExecuteChanged();
            }
        }

        /// <inheritdoc/>
        public bool CanExecute()
        {
            return !this.IsExecuting && (this.canExecute?.Invoke() ?? true);
        }

        /// <inheritdoc/>
        public async Task ExecuteAsync()
        {
            if (this.CanExecute())
            {
                if (this.execute is not null)
                {
                    try
                    {
                        this.IsExecuting = true;
                        await this.execute().ConfigureAwait(false);
                    }
                    finally
                    {
                        this.IsExecuting = false;
                    }
                }
            }
        }

        /// <summary>
        /// Raises Can Execute Changed.
        /// </summary>
#pragma warning disable CA1030 // Use events where appropriate
        public void RaiseCanExecuteChanged()
#pragma warning restore CA1030 // Use events where appropriate
        {
            Microsoft.Maui.Controls.Application.Current?.Dispatcher.Dispatch(() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty));
        }

        /// <inheritdoc/>
        bool ICommand.CanExecute(object? parameter)
        {
            return this.CanExecute();
        }

        /// <inheritdoc/>
        void ICommand.Execute(object? parameter)
        {
            this.ExecuteAsync().FireAndForgetSafeAsync(this.errorHandler);
        }
    }
}
