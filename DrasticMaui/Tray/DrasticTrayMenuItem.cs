using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrasticMaui
{
    public class DrasticTrayMenuItem
    {
        public DrasticTrayMenuItem (string text, Stream? icon = null)
        {
            this.Text = text;
            this.Icon = icon;
        }

        public string Text { get; }

        public Stream? Icon { get; }
    }
}
