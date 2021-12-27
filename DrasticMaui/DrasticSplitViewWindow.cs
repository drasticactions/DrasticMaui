// <copyright file="DrasticSplitViewWindow.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;

namespace DrasticMaui
{
    /// <summary>
    /// Drastic Split View Window.
    /// </summary>
    public partial class DrasticSplitViewWindow : DrasticMauiWindow
    {
        private IMauiContext context;
        private Page menu;
        private bool isInitialized;

        public DrasticSplitViewWindow(Page menu, Page content, IMauiContext context)
        {
            this.context = context;
            this.Page = content;
            this.menu = menu;
        }

        protected override void OnHandlerChanged()
        {
            base.OnHandlerChanged();
            if (!this.isInitialized)
            {
                this.SetupSplitView();
            }
        }

#if !__MACCATALYST__ && !__IOS__
        public void SetupSplitView()
        {
        }
#endif
    }
}