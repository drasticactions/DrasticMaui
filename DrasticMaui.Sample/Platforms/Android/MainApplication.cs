// <copyright file="MainApplication.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Android.App;
using Android.Runtime;

namespace DrasticMaui.Sample;

/// <summary>
/// Main Application.
/// </summary>
[Application]
public class MainApplication : MauiApplication
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainApplication"/> class.
    /// </summary>
    /// <param name="handle">Application Handle Pointer.</param>
    /// <param name="ownership"><see cref="JniHandleOwnership"/>.</param>
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    /// <inheritdoc/>
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
