// <copyright file="ExtendedBindableObject.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;

namespace DrasticMaui.Tools
{
    /// <summary>
    /// Extended Bindable Object.
    /// </summary>
    public class ExtendedBindableObject : BindableObject
    {
        /// <summary>
        /// Set Property.
        /// </summary>
        /// <typeparam name="T">Generic Type.</typeparam>
        /// <param name="backingStore">Backing Store.</param>
        /// <param name="value">Value.</param>
        /// <param name="propertyName">Property Name.</param>
        /// <returns>Boolean.</returns>
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            this.OnPropertyChanged(propertyName);

            return true;
        }
    }
}
