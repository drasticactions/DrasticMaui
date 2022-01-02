// <copyright file="IAsyncCommand.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DrasticMaui.Tools
{
    /// <summary>
    /// IAsyncCommand.
    /// </summary>
    public interface IAsyncCommand : ICommand
    {
        /// <summary>
        /// Execute Async.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        Task ExecuteAsync();

        /// <summary>
        /// Can execute Command.
        /// </summary>
        /// <returns>Boolean.</returns>
        bool CanExecute();
    }
}
