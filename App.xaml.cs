using SaveUpApp.ViewModels;
using System.Globalization;

namespace SaveUpApp
{
    public partial class App : Application
    {
        public static SaveUpViewModel SharedViewModel { get; } = new SaveUpViewModel();
        public static ShellViewModel ShellViewModel { get; } = new();

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        public static void ChangeLanguage(string cultureCode)
        {
            var culture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentUICulture = culture;
            SaveUpApp.Resources.Languages.Resources.Culture = culture;
        }


    }
}
