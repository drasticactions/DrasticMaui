// <copyright file="PageOverlay.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrasticMaui.Tools;
using Microsoft.Maui.Platform;

namespace DrasticMaui.Overlays
{
    /// <summary>
    /// Page Overlay.
    /// </summary>
    public partial class PageOverlay : BaseOverlay, IVisualTreeElement
    {
        private bool pageOverlayNativeElementsInitialized;
        private HashSet<Page> views = new HashSet<Page>();
        private Dictionary<Page, List<IView>> hitTestElements = new Dictionary<Page, List<IView>>();
        private IMauiContext? context;

        /// <summary>
        /// Initializes a new instance of the <see cref="PageOverlay"/> class.
        /// </summary>
        /// <param name="window"><see cref="IWindow"/>.</param>
        public PageOverlay(IWindow window)
            : base(window)
        {
        }

        /// <summary>
        /// Gets a read only list of the pages on the overlay.
        /// </summary>
        public IReadOnlyList<Page> Views => this.views.ToList().AsReadOnly();

        /// <summary>
        /// Gets hit of hit testable elements.
        /// </summary>
        public IReadOnlyList<IView> HitTestElements => this.hitTestElements.SelectMany(n => n.Value).ToList().AsReadOnly();

        /// <summary>
        /// Add View.
        /// </summary>
        /// <param name="view">View.</param>
        /// <returns>Boolean.</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException.</exception>
        public bool AddView(Page view)
        {
            var result = this.views.Add(view);
            if (!result)
            {
                return false;
            }
#if IOS || WINDOWS || ANDROID || MACCATALYST
            this.AddNativeElements(view);
#endif
            if (view is IHitTestView hittestView)
            {
                this.hitTestElements.Add(view, hittestView.HitTestViews);
            }
            
            Microsoft.Maui.Controls.Xaml.Diagnostics.VisualDiagnostics.OnChildAdded(this, view, 0);
            return true;
        }

        /// <summary>
        /// Remove View.
        /// </summary>
        /// <param name="view">View.</param>
        /// <returns>Bool.</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException.</exception>
        public bool RemoveView(Page view)
        {
            if (view is not IHitTestView)
            {
                throw new ArgumentNullException(nameof(view));
            }

            var result = this.views.Remove(view);
            if (!result)
            {
                return false;
            }

#if IOS || WINDOWS || ANDROID || MACCATALYST
            this.RemoveNativeElements(view);
#endif

            Microsoft.Maui.Controls.Xaml.Diagnostics.VisualDiagnostics.OnChildRemoved(this, view, 0);
            return this.hitTestElements.Remove(view);
        }

        /// <summary>
        /// Remove Views.
        /// </summary>
        public void RemoveViews()
        {
            this.views.Clear();
            this.hitTestElements.Clear();
        }

        /// <inheritdoc/>
        public IReadOnlyList<IVisualTreeElement> GetVisualChildren()
        {
            var elements = new List<IVisualTreeElement>();
            foreach (var page in this.views)
            {
                if (page is IVisualTreeElement element)
                {
                    elements.AddRange(element.GetVisualChildren());
                }
            }

            return elements;
        }

        /// <inheritdoc/>
        public IVisualTreeElement? GetVisualParent()
        {
            return this.Window as IVisualTreeElement;
        }
    }
}
