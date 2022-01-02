// <copyright file="BaseViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DrasticMaui.Services;
using DrasticMaui.Tools;

namespace DrasticMaui.ViewModels
{
    /// <summary>
    /// Base View Model.
    /// </summary>
    public class BaseViewModel : ExtendedBindableObject
    {
        private bool isBusy;
        private bool isRefreshing;
        private string? title;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        /// <param name="navigation">Navigation Service.</param>
        /// <param name="services">IServiceProvider.</param>
        public BaseViewModel(INavigationService navigation, IServiceProvider services)
        {
            this.Services = services;
            var error = services.GetService<IErrorHandlerService>();

            if (navigation is null)
            {
                throw new ArgumentNullException(nameof(navigation));
            }

            if (error is null)
            {
                throw new ArgumentNullException(nameof(error));
            }

            this.Navigation = navigation;
            this.Error = error;

            this.CloseDialogCommand = new AsyncCommand(
               async () => await this.ExecutePopModalCommand(),
               null,
               this.Error);
        }

        /// <summary>
        /// Gets or Sets a value indicating whether the view is busy.
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }

            set
            {
                this.isBusy = value;
                this.OnPropertyChanged(nameof(this.IsBusy));
            }
        }

        /// <summary>
        /// Gets or Sets a value indicating whether the view is refreshing.
        /// </summary>
        public bool IsRefreshing
        {
            get
            {
                return this.isRefreshing;
            }

            set
            {
                this.isRefreshing = value;
                this.OnPropertyChanged(nameof(this.IsRefreshing));
            }
        }

        /// <summary>
        /// Gets or sets the page title.
        /// </summary>
        public string? Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                this.OnPropertyChanged(nameof(this.Title));
            }
        }

        /// <summary>
        /// Gets the Close Dialog Command.
        /// </summary>
        public AsyncCommand CloseDialogCommand { get; private set; }

        /// <summary>
        /// Gets the service provider collection.
        /// </summary>
        protected IServiceProvider Services { get; private set; }

        /// <summary>
        /// Gets the navigation service.
        /// </summary>
        protected INavigationService Navigation { get; private set; }

        /// <summary>
        /// Gets the error handler service.
        /// </summary>
        protected IErrorHandlerService Error { get; private set; }

        /// <summary>
        /// Load VM Async.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        public virtual Task LoadAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Sets title for page.
        /// </summary>
        /// <param name="title">The Title.</param>
        public virtual void SetTitle(string title = "")
        {
            this.Title = title;
        }

        /// <summary>
        /// Unload VM Async.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        public virtual Task UnloadAsync()
        {
            return Task.CompletedTask;
        }

        private Task ExecutePopModalCommand() =>
            this.Navigation.PopModalAsync();
    }
}
