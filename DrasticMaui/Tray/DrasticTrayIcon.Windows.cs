using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrasticMaui
{
    public partial class DrasticTrayIcon
    {
        private NotifyIcon? notifyIcon;
        private Icon? icon;

        private void SetupStatusBarButton()
        {
            this.notifyIcon = new NotifyIcon();
            this.notifyIcon.Icon = this.icon;
            this.notifyIcon.Text = this.iconName;
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += this.NotifyIcon_MouseClick;
        }

        private void NotifyIcon_MouseClick(object? sender, MouseEventArgs e)
        {
            this.Clicked?.Invoke(this, EventArgs.Empty);
        }

        private void SetupStatusBarImage()
        {
            if (this.iconStream is not null)
            {
                this.icon = new Icon(this.iconStream);
            }
        }
    }
}
