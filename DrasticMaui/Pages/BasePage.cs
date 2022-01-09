// <copyright file="BasePage.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DrasticMaui.ViewModels;

namespace DrasticMaui
{
    /// <summary>
    /// Base Page.
    /// </summary>
    public class BasePage : ContentPage
    {
        private BaseViewModel? viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePage"/> class.
        /// </summary>
        /// <param name="services">IServiceProvider.</param>
        public BasePage(IServiceProvider services)
        {
            this.Services = services;
        }

        /// <summary>
        /// Gets or sets the View Model.
        /// </summary>
        public BaseViewModel? ViewModel
        {
            get
            {
                return this.viewModel;
            }

            set
            {
                this.viewModel = value;
                if (value is not null)
                {
                    this.BindingContext = this.viewModel;
                }
            }
        }

        /// <summary>
        /// Gets or sets the IServiceProvider.
        /// </summary>
        public IServiceProvider Services { get; set; }

        /// <inheritdoc/>
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (this.ViewModel != null)
            {
                await this.ViewModel.LoadAsync();
            }
        }
    }
}
