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
    public class DrasticMauiWindow : Window, IVisualTreeElement
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

            return elements;
        }

        /// <inheritdoc/>
        public IVisualTreeElement? GetVisualParent() => Application.Current;
    }
}
