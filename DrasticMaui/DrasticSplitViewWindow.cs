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
        private Page menu;
        private bool isInitialized;

        public DrasticSplitViewWindow(Page menu, Page content)
        {
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

        internal override void AddVisualChildren(List<IVisualTreeElement> elements)
        {
            if (this.menu is not null && this.menu is IVisualTreeElement element)
            {
                elements.AddRange(element.GetVisualChildren().ToList());
            }
        }

#if !__MACCATALYST__ && !__IOS__ && !WINDOWS
        public void SetupSplitView()
        {
        }
#endif
    }
}