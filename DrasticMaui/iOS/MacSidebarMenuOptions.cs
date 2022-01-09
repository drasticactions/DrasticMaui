using UIKit;

namespace DrasticMaui.iOS
{
    public class MacSidebarMenuOptions
    {
        public MacSidebarMenuOptions(UINavigationItemLargeTitleDisplayMode largeTitle = UINavigationItemLargeTitleDisplayMode.Always)
        {
            this.LargeTitleDisplayMode = largeTitle;
        }

        public UINavigationItemLargeTitleDisplayMode LargeTitleDisplayMode { get; }
    }
}