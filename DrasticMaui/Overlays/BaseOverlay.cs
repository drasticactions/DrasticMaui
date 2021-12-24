// <copyright file="BaseOverlay.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrasticMaui.Overlays
{
    /// <summary>
    /// Base Overlay.
    /// Used to host other overlays.
    /// </summary>
    public class BaseOverlay : IWindowOverlay
    {
        private HashSet<IWindowOverlayElement> overlayElements;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseOverlay"/> class.
        /// </summary>
        /// <param name="window"><see cref="IWindow"/>.</param>
        public BaseOverlay(IWindow window)
        {
            this.Window = window;
            this.overlayElements = new HashSet<IWindowOverlayElement>();
        }

        /// <inheritdoc/>
        public event EventHandler<WindowOverlayTappedEventArgs>? Tapped;

        /// <inheritdoc/>
        public bool DisableUITouchEventPassthrough { get; set; }

        /// <inheritdoc/>
        public bool EnableDrawableTouchHandling { get; set; }

        /// <inheritdoc/>
        public bool IsVisible { get; set; }

        /// <inheritdoc/>
        public IWindow Window { get; }

        /// <inheritdoc/>
        public float Density { get; set; }

        /// <inheritdoc/>
        public IReadOnlyCollection<IWindowOverlayElement> WindowElements => this.overlayElements.ToList().AsReadOnly();

        /// <inheritdoc/>
        public bool IsNativeViewInitialized { get; private set; }

        /// <inheritdoc/>
        public virtual bool AddWindowElement(IWindowOverlayElement element)
        {
            return this.overlayElements.Add(element);
        }

        /// <inheritdoc/>
        public virtual bool Deinitialize()
        {
            this.DeinitializeNativeDependencies();
            return true;
        }

        /// <inheritdoc/>
        public virtual void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
        }

        /// <inheritdoc/>
        public virtual void HandleUIChange()
        {
        }

        /// <inheritdoc/>
        public virtual void Invalidate()
        {
        }

        /// <inheritdoc/>
        public bool RemoveWindowElement(IWindowOverlayElement element)
        {
            return this.overlayElements.Remove(element);
        }

        /// <inheritdoc/>
        public void RemoveWindowElements()
        {
            this.overlayElements.Clear();
        }

        /// <inheritdoc/>
        public virtual bool Initialize()
        {
            return false;
        }

        /// <summary>
        /// Deinitialize Native Dependencies.
        /// </summary>
        public virtual void DeinitializeNativeDependencies()
        {
        }
    }
}
