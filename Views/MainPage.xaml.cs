using SaveUpApp.ViewModels;

namespace SaveUpApp.Views;
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = App.SharedViewModel;
        }

        private void OnGermanClicked(object sender, EventArgs e)
        {
            App.ShellViewModel.SetLanguage("de");
        }

        private void OnEnglishClicked(object sender, EventArgs e)
        {
            App.ShellViewModel.SetLanguage("en");
        }


    }


