using System;
using UIKit;

namespace DrasticMaui.iOS
{
    public class MacSidebarMenuOptions
    {
        public MacSidebarMenuOptions(UINavigationItemLargeTitleDisplayMode largeTitle)
        {
            this.LargeTitleDisplayMode = largeTitle;
        }

        public UINavigationItemLargeTitleDisplayMode LargeTitleDisplayMode { get; }
    }
}