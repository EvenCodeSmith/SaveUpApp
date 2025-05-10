using SaveUpApp.ViewModels;

namespace SaveUpApp
{
    public partial class App : Application
    {
        public static SaveUpViewModel SharedViewModel { get; } = new SaveUpViewModel(); // Ich hatte Ghost Data ...

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
