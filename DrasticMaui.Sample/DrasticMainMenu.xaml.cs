using DrasticMaui.Models;

namespace DrasticMaui.Sample;

public partial class DrasticMainMenu : ContentPage
{
    public DrasticMainMenu()
    {
        InitializeComponent();
        this.CollectionViewTest.ItemsSource = this.MenuItems = new List<DrasticMenuItem>()
        {
            new DrasticMenuItem("Test"),
            new DrasticMenuItem("Test2")
        };
    }

    public List<DrasticMenuItem> MenuItems { get; set; }
}