using SaveUpApp.ViewModels;

namespace SaveUpApp.Views
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new SaveUpViewModel();
        }

    }

}
