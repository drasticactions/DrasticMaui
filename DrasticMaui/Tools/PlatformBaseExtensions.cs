// <copyright file="PlatformBaseExtensions.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Reflection;
using DrasticMaui.Overlays;

namespace DrasticMaui.Tools
{
    /// <summary>
    /// Platform Base Extensions.
    /// </summary>
    public static class PlatformBaseExtensions
    {
        /// <summary>
        /// Get all field values from an obj.
        /// </summary>
        /// <param name="obj">Obj.</param>
        /// <returns>Field Values.</returns>
        public static Dictionary<string, string?> GetFieldValues(this object obj)
            => obj.GetType().GetFieldValues();

        /// <summary>
        /// Get Field Values.
        /// </summary>
        /// <param name="type">Type.</param>
        /// <returns>Field Values.</returns>
        public static Dictionary<string, string?> GetFieldValues(this Type type)
        {
            return type
                      .GetFields(BindingFlags.Public | BindingFlags.Static)
                      .Where(f => f.FieldType == typeof(string))
                      .ToDictionary(
                          f => f.Name,
                          f => f.GetValue(null) as string);
        }
    }
}
