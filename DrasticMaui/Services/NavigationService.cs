// <copyright file="NavigationService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DrasticMaui.Services
{
    /// <summary>
    /// Navigation Service.
    /// </summary>
    public class NavigationService : INavigationService
    {
        public NavigationService(DrasticMauiWindow window)
        {
            this.Window = window;
        }

        /// <inheritdoc/>
        public DrasticMauiWindow Window { get; }

        /// <inheritdoc/>
        public Task PopModalAsync()
        {
            return this.Window.Navigation.PopModalAsync();
        }
    }
}
