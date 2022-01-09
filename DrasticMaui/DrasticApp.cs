// <copyright file="DrasticApp.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DrasticMaui
{
    /// <summary>
    /// Drastic App.
    /// </summary>
    public class DrasticApp : Microsoft.Maui.Controls.Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrasticApp"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/>.</param>
        public DrasticApp(IServiceProvider services)
        {
            this.Services = services;
        }

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        protected IServiceProvider Services { get; }
    }
}
