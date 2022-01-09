namespace DrasticMaui.Sample
{
    public partial class MauiTestPage : ContentPage
    {
        private int count = 0;

        public MauiTestPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
            CounterLabel.Text = $"Current count: {count}";

            SemanticScreenReader.Announce(CounterLabel.Text);
        }
    }
}
