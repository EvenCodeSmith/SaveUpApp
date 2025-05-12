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

    private void OnCurrencyChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        string selected = picker.SelectedItem?.ToString();

        if (!string.IsNullOrWhiteSpace(selected))
        {
            App.ShellViewModel.CurrencySymbol = selected;

            foreach (var item in App.SharedViewModel.Items)
            {
                item.NotifyCurrencyChanged();
            }

        }
        Application.Current.MainPage = new AppShell();
    }

}