using SaveUpApp.ViewModels;
using System.Globalization;

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

        public static void ChangeLanguage(string cultureCode)
        {
            var culture = new CultureInfo(cultureCode);
            Thread.CurrentThread.CurrentUICulture = culture;

            // ganz klar sagen, welche Resources gemeint sind
            SaveUpApp.Resources.Languages.Resources.Culture = culture;
        }


    }
}
