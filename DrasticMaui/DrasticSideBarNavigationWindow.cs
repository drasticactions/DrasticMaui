// <copyright file="DrasticSideBarNavigationWindow.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DrasticMaui.Models;

namespace DrasticMaui
{
    public partial class DrasticSideBarNavigationWindow : DrasticMauiWindow
    {
        private SidebarMenuOptions options;
        private bool isInitialized;

#if !IOS && !MACCATALYST && !WINDOWS
        public DrasticSideBarNavigationWindow(
           Page content,
           SidebarMenuOptions options,
           IServiceProvider services)
           : base(services)
        {
            this.Page = content;
            this.options = options;
        }

        /// <summary>
        /// Setup Navigation View.
        /// </summary>
        public void SetupNavigationView()
        {
        }
#endif

        /// <inheritdoc/>
        protected override void OnHandlerChanged()
        {
            base.OnHandlerChanged();
            if (!this.isInitialized)
            {
                this.SetupNavigationView();
            }
        }
    }
}
