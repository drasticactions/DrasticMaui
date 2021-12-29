using DrasticMaui.Models;

namespace DrasticMaui.Sample;

public partial class DrasticMainMenu : ContentPage
{
    public DrasticMainMenu()
    {
        InitializeComponent();
        this.CollectionViewTest.ItemsSource = MenuItems;
    }

    public List<DrasticMenuItem> MenuItems { get; set; } = new List<DrasticMenuItem>() { new DrasticMenuItem("Test", new MainPage()) };
}