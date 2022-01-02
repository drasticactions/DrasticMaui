using System;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace DrasticMaui.Sample
{
    public partial class TraySample : ContentPage
    {
        int count = 0;

        public TraySample()
        {
            InitializeComponent();
        }

        private void OnReset(object sender, EventArgs e)
        {
            if (this.GetParentWindow() is DrasticTrayWindow win)
            {
                win.Setup();
            }
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
            CounterLabel.Text = $"Current count: {count}";

            SemanticScreenReader.Announce(CounterLabel.Text);
        }
    }
}