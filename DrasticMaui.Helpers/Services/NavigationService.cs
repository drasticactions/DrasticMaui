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
        /// <inheritdoc/>
        public Task PopModalAsync(Window window)
        {
            return window.Navigation.PopModalAsync();
        }
    }
}
