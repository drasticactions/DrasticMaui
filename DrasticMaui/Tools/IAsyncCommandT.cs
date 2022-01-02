// <copyright file="IAsyncCommandT.cs" company="Drastic Actions">
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
    /// <typeparam name="T">Type of Command.</typeparam>
#pragma warning disable SA1649 // File name should match first type name
    public interface IAsyncCommand<T> : ICommand
#pragma warning restore SA1649 // File name should match first type name
    {
        /// <summary>
        /// Execute Async.
        /// </summary>
        /// <param name="parameter">parameter.</param>
        /// <returns><see cref="Task"/>.</returns>
        Task ExecuteAsync(T parameter);

        /// <summary>
        /// Can Execute.
        /// </summary>
        /// <param name="parameter">parameter.</param>
        /// <returns>Bool.</returns>
        bool CanExecute(T parameter);
    }
}
