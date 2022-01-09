using DrasticMaui.Models;

namespace DrasticMaui.Sample;

public partial class DrasticMainMenu : ContentPage
{
    public DrasticMainMenu()
    {
        InitializeComponent();
        this.CollectionViewTest.ItemsSource = MenuItems;
    }

    public List<NavigationSidebarItem> MenuItems { get; set; } = new List<NavigationSidebarItem>() { };
}