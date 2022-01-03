// <copyright file="MainPageViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrasticMaui.Services;
using DrasticMaui.Tools;
using DrasticMaui.ViewModels;

namespace DrasticMaui.Sample.ViewModels
{
    /// <summary>
    /// Main page View Model.
    /// </summary>
    public class MainPageViewModel : BaseViewModel
    {
        private bool isFullScreen;

        private AsyncCommand? addPageOverlayCommand;
        private AsyncCommand? addNewWindowCommand;
        private AsyncCommand? showFullScreenCommand;
        private AsyncCommand? testTrayIconCommand;
        private AsyncCommand? newTrayAppCommand;
        private AsyncCommand? setupTrayIconCommand;
        private AsyncCommand? navigatePageCommand;

        private PageOverlaySample? sample;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel"/> class.
        /// </summary>
        /// <param name="services">IServiceProvider.</param>
        /// <param name="originalPage">Original Page.</param>
        public MainPageViewModel(IServiceProvider services, Page? originalPage = null)
            : base(services, originalPage)
        {
            this.Title = "DrasticMaui";
        }

        /// <summary>
        /// Gets the add page overlay command.
        /// </summary>
        public AsyncCommand AddPageOverlayCommand
        {
            get
            {
                return this.addPageOverlayCommand ??= new AsyncCommand(this.AddPageOverlay, null, this.Error);
            }
        }

        /// <summary>
        /// Gets the add new window command.
        /// </summary>
        public AsyncCommand AddNewWindowCommand
        {
            get
            {
                return this.addNewWindowCommand ??= new AsyncCommand(this.AddNewWindow, null, this.Error);
            }
        }

        /// <summary>
        /// Gets the full screen command.
        /// </summary>
        public AsyncCommand ShowFullScreenCommand
        {
            get
            {
                return this.showFullScreenCommand ??= new AsyncCommand(this.ShowFullScreen, null, this.Error);
            }
        }

        /// <summary>
        /// Gets the test tray icon command.
        /// </summary>
        public AsyncCommand TestTrayIconCommand
        {
            get
            {
                return this.testTrayIconCommand ??= new AsyncCommand(this.NewTrayApp, null, this.Error);
            }
        }

        /// <summary>
        /// Gets the new tray app command.
        /// </summary>
        public AsyncCommand NewTrayAppCommand
        {
            get
            {
                return this.newTrayAppCommand ??= new AsyncCommand(this.TestTrayIcon, null, this.Error);
            }
        }

        /// <summary>
        /// Gets the setup tray app command.
        /// </summary>
        public AsyncCommand SetupTrayIconCommand
        {
            get
            {
                return this.setupTrayIconCommand ??= new AsyncCommand(this.SetupTrayApp, null, this.Error);
            }
        }

        /// <summary>
        /// Gets the navigate page command.
        /// </summary>
        public AsyncCommand NavigatePageCommand
        {
            get
            {
                return this.navigatePageCommand ??= new AsyncCommand(this.NavigatePage, null, this.Error);
            }
        }

        private async Task AddNewWindow()
        {
            var newWindow = new DrasticMauiWindow(this.Services) { Page = new MainPage(this.Services) };
            Application.Current?.OpenWindow(newWindow);
        }

        private async Task AddPageOverlay()
        {
            if (this.HostedWindow is not DrasticMauiSampleWindow window)
            {
                return;
            }

            this.sample ??= new PageOverlaySample(window, this.Services);
            window.PageOverlay.AddView(this.sample);
        }

        private async Task ShowFullScreen()
        {
            if (this.HostedWindow is not DrasticMauiSampleWindow window)
            {
                return;
            }

            this.isFullScreen = !isFullScreen;
            window.ToggleFullScreen(isFullScreen);
        }

        private async Task TestTrayIcon()
        {
            if (this.HostedWindow is not DrasticMauiSampleWindow window)
            {
                return;
            }

            window.TestTrayIcon();
        }

        private async Task NewTrayApp()
        {
            if (this.HostedWindow is not DrasticMauiSampleWindow window)
            {
                return;
            }

            window.EnableTrayApp();
        }

        private async Task SetupTrayApp()
        {
            if (this.HostedWindow is not DrasticMauiSampleWindow window)
            {
                return;
            }

            window.SetupTrayIcon();
        }

        private async Task NavigatePage()
        {
            if (this.HostedWindow is not DrasticMauiSampleWindow window)
            {
                return;
            }
        }
    }
}
