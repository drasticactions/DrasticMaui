using System;

namespace DrasticMaui.Models
{
	public class DrasticMenuItem
	{
		public DrasticMenuItem(string text)
		{
			this.Text = text;
		}

		/// <summary>
        /// Gets or sets the text for the menu item.
        /// </summary>
		public string Text { get; set; }
	}
}

