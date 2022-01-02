// <copyright file="TaskCompletionNotifier.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Taken from https://stackoverflow.com/questions/15003827/async-implementation-of-ivalueconverter
namespace DrasticMaui.Tools
{
    /// <summary>
    /// Task Completion Notifier.
    /// </summary>
    /// <typeparam name="TResult">Task.</typeparam>
    public sealed class TaskCompletionNotifier<TResult> : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskCompletionNotifier{TResult}"/> class.
        /// </summary>
        /// <param name="task">Task.</param>
        public TaskCompletionNotifier(Task<TResult> task)
        {
            Task = task;
            if (!task.IsCompleted)
            {
                var scheduler = (SynchronizationContext.Current == null) ? TaskScheduler.Current : TaskScheduler.FromCurrentSynchronizationContext();
                task.ContinueWith(
                    t =>
                {
                    var propertyChanged = this.PropertyChanged;
                    if (propertyChanged != null)
                    {
                        propertyChanged(this, new PropertyChangedEventArgs("IsCompleted"));
                        if (t.IsCanceled)
                        {
                            propertyChanged(this, new PropertyChangedEventArgs("IsCanceled"));
                        }
                        else if (t.IsFaulted)
                        {
                            propertyChanged(this, new PropertyChangedEventArgs("IsFaulted"));
                            propertyChanged(this, new PropertyChangedEventArgs("ErrorMessage"));
                        }
                        else
                        {
                            propertyChanged(this, new PropertyChangedEventArgs("IsSuccessfullyCompleted"));
                            propertyChanged(this, new PropertyChangedEventArgs("Result"));
                        }
                    }
                },
                    CancellationToken.None,
                    TaskContinuationOptions.ExecuteSynchronously,
                    scheduler);
            }
        }

        /// <summary>
        /// Property Changed Event.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Gets the task being watched. This property never changes and is never <c>null</c>
        /// </summary>
        public Task<TResult> Task { get; private set; }

        /// <summary>
        /// Gets the result of the task. Returns the default value of TResult if the task has not completed successfully.
        /// </summary>
        public TResult? Result { get { return (this.Task.Status == TaskStatus.RanToCompletion) ? this.Task.Result : default(TResult); } }

        /// <summary>
        /// Gets a value indicating whether the task has completed.
        /// </summary>
        public bool IsCompleted { get { return this.Task.IsCompleted; } }

        /// <summary>
        /// Gets a value indicating whether the task has completed successfully.
        /// </summary>
        public bool IsSuccessfullyCompleted { get { return this.Task.Status == TaskStatus.RanToCompletion; } }

        /// <summary>
        /// Gets a value indicating whether the task has been canceled.
        /// </summary>
        public bool IsCanceled { get { return Task.IsCanceled; } }

        /// <summary>
        /// Gets a value indicating whether the task has faulted.
        /// </summary>
        public bool IsFaulted { get { return this.Task.IsFaulted; } }

    }
}
