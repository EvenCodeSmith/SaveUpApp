using SaveUpApp.ViewModels;

namespace SaveUpApp.Views;

public partial class AddItemPage : ContentPage
{
    public AddItemPage()
    {
        InitializeComponent();
        BindingContext = App.SharedViewModel;
    }
}
