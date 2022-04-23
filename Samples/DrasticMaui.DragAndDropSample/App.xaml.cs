// <copyright file="App.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DrasticMaui.DragAndDropSample;

/// <summary>
/// MAUI App.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        this.InitializeComponent();
    }

    /// <inheritdoc/>
    protected override Window CreateWindow(IActivationState? activationState)
        => new DragAndDropWindow() { Page = new MainPage() };

    /// <summary>
    /// Drag And Drop Window.
    /// </summary>
    public class DragAndDropWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DragAndDropWindow"/> class.
        /// </summary>
        public DragAndDropWindow()
        {
            this.DragAndDropOverlay = new DrasticMaui.Overlays.DragAndDropOverlay(this);
        }

        /// <summary>
        /// Gets the drag and drop overlay.
        /// </summary>
        internal Overlays.DragAndDropOverlay DragAndDropOverlay { get; }

        /// <inheritdoc/>
        protected override void OnCreated()
        {
            this.AddOverlay(this.DragAndDropOverlay);
        }
    }
}
