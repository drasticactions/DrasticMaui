// <copyright file="WindowTappedService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrasticMaui.Services
{
    /// <summary>
    /// Window Tapped Service.
    /// </summary>
    public class WindowTappedService : IWindowTappedService
    {
        private const int CursorHiddenAfterSeconds = 4;
        private readonly IWindow window;
        private bool screenTapped;
        private bool wasInvoked;
        private PeriodicTimer? timer;
        private CancellationTokenSource? cts;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowTappedService"/> class.
        /// </summary>
        /// <param name="window">Window.</param>
        public WindowTappedService(IWindow window)
        {
            this.window = window;
            this.window.VisualDiagnosticsOverlay.Tapped += this.VisualDiagnosticsOverlay_Tapped;
        }

        /// <inheritdoc/>
        public event EventHandler? OnHidden;

        /// <inheritdoc/>
        public event EventHandler? OnTapped;

        /// <inheritdoc/>
        public void StartService()
        {
            this.cts = new CancellationTokenSource();
            this.timer = new PeriodicTimer(TimeSpan.FromSeconds(CursorHiddenAfterSeconds));
            this.wasInvoked = true;
            Task.Run(this.TimerTask, this.cts.Token);
        }

        /// <inheritdoc/>
        public void StopService()
        {
            this.cts?.Cancel();
            this.OnTapped?.Invoke(this, EventArgs.Empty);
        }

        private async Task TimerTask()
        {
            if (this.timer == null || (this.cts != null && this.cts.IsCancellationRequested))
            {
                return;
            }

            try
            {
                if (this.cts is not null)
                {
                    while (await this.timer.WaitForNextTickAsync(this.cts.Token))
                    {
                        if (this.screenTapped && this.wasInvoked)
                        {
                            this.screenTapped = false;
                        }
                        else if (!this.screenTapped && this.wasInvoked)
                        {
                            this.OnHidden?.Invoke(this, EventArgs.Empty);
                            this.wasInvoked = false;
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Good, we expect this.
            }
        }

        private void VisualDiagnosticsOverlay_Tapped(object? sender, WindowOverlayTappedEventArgs e)
        {
            this.screenTapped = true;
            this.wasInvoked = true;
            this.OnTapped?.Invoke(this, EventArgs.Empty);
        }
    }
}