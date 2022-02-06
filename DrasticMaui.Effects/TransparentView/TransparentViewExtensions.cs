// <copyright file="TransparentViewExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DrasticMaui.Effects
{
    public static class TransparentViewExtensions
    {
        public static void ToTransparent(this IView view, IMauiContext context)
        {
#if WINDOWS10_0_19041_0_OR_GREATER
            view.ApplyAcrylic(context);
#endif
        }
    }
}
