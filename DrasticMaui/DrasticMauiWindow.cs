// <copyright file="DrasticMauiWindow.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrasticMaui
{
    /// <summary>
    /// Drastic Maui Window.
    /// </summary>
    public partial class DrasticMauiWindow : Window, IVisualTreeElement
    {
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
        internal virtual void AddVisualChildren(List<IVisualTreeElement> elements)
        {
        }
    }
}
