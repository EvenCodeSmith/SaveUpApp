using SaveUpApp.ViewModels;


namespace SaveUpApp.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
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

    private void OnCrashButtonClicked(object sender, EventArgs e)
    {
        object o7 = null;
        var explode = o7.ToString(); // BUMMM :)
    }
}