using SaveUpApp.ViewModels;

namespace SaveUpApp.Views;

public partial class ListPage : ContentPage
{
    public ListPage()
    {
        InitializeComponent();
        BindingContext = App.SharedViewModel;
    }
}
