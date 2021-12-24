// <copyright file="IHitTestView.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DrasticMaui.Overlays
{
    /// <summary>
    /// Hit Test Views.
    /// </summary>
    public interface IHitTestView
    {
        /// <summary>
        /// Gets the list of hit test views.
        /// </summary>
        List<IView> HitTestViews { get; }
    }
}
