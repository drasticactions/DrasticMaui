// <copyright file="DrasticWindow.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DrasticMaui
{
    /// <summary>
    /// Drastic Maui Window.
    /// Contains helper functions for handling custom views on top of Windows.
    /// </summary>
    public partial class DrasticWindow : Window, IVisualTreeElement
    {
        private IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrasticWindow"/> class.
        /// </summary>
        /// <param name="services">Service Provider.</param>
        public DrasticWindow(IServiceProvider services)
        {
            this.serviceProvider = services;
        }

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        public IServiceProvider ServiceProvider => this.serviceProvider;

        /// <inheritdoc/>
        public IReadOnlyList<IVisualTreeElement> GetVisualChildren()
        {
            var elements = new List<IVisualTreeElement>();
            if (this.Page != null && this.Page is IVisualTreeElement element)
            {
                elements.AddRange(element.GetVisualChildren());
            }

            var overlays = this.Overlays.Where(n => n is IVisualTreeElement).Cast<IVisualTreeElement>();
            elements.AddRange(overlays);

            this.AddVisualChildren(elements);

            return elements;
        }

        /// <inheritdoc/>
        public IVisualTreeElement? GetVisualParent() => Microsoft.Maui.Controls.Application.Current;

        /// <summary>
        /// Add Visual Children to the window.
        /// <see cref="IVisualTreeElement"/> allow the Live Visual Tree
        /// to see elements contained within the subviews of your application.
        /// If you add custom views to your app, you can wire them in here so they can work with LVT.
        /// </summary>
        /// <param name="elements">Elements.</param>
        public virtual void AddVisualChildren(List<IVisualTreeElement> elements)
        {
        }
    }
}
