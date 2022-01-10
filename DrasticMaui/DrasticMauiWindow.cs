// <copyright file="DrasticMauiWindow.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DrasticMaui
{
    /// <summary>
    /// Drastic Maui Window.
    /// </summary>
    public partial class DrasticMauiWindow : Window, IVisualTreeElement
    {
        private IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrasticMauiWindow"/> class.
        /// </summary>
        /// <param name="services">Service Provider.</param>
        public DrasticMauiWindow(IServiceProvider services)
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

#if !MACCATALYST && !WINDOWS
        /// <summary>
        /// Toggle Full Screen Support.
        /// </summary>
        /// <param name="fullScreen">Enable Full Screen.</param>
        public void ToggleFullScreen(bool fullScreen)
        {
        }
#endif

        /// <inheritdoc/>
        public IVisualTreeElement? GetVisualParent() => Microsoft.Maui.Controls.Application.Current;

        /// <summary>
        /// Add Visual Children.
        /// </summary>
        /// <param name="elements">Elements.</param>
        public virtual void AddVisualChildren(List<IVisualTreeElement> elements)
        {
        }
    }
}
