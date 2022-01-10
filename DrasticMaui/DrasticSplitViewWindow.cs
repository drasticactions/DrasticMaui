// <copyright file="DrasticSplitViewWindow.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace DrasticMaui
{
    /// <summary>
    /// Drastic Split View Window.
    /// </summary>
    public partial class DrasticSplitViewWindow : DrasticMauiWindow
    {
        private Page menu;
        private bool isInitialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrasticSplitViewWindow"/> class.
        /// </summary>
        /// <param name="menu">Pane Content.</param>
        /// <param name="content">Main Content.</param>
        /// <param name="services">Services.</param>
        public DrasticSplitViewWindow(Page menu, Page content, IServiceProvider services)
            : base(services)
        {
            this.Page = content;
            this.menu = menu;
        }

#if !__MACCATALYST__ && !__IOS__ && !WINDOWS
        /// <summary>
        /// Setup Split View.
        /// </summary>
        public void SetupSplitView()
        {
        }
#endif

        /// <inheritdoc/>
        public override void AddVisualChildren(List<IVisualTreeElement> elements)
        {
            if (this.menu is not null && this.menu is IVisualTreeElement element)
            {
                elements.AddRange(element.GetVisualChildren().ToList());
            }
        }

        /// <inheritdoc/>
        protected override void OnHandlerChanged()
        {
            base.OnHandlerChanged();
            if (!this.isInitialized)
            {
                this.SetupSplitView();
            }
        }
    }
}