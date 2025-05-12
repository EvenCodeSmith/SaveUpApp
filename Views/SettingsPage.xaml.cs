using SaveUpApp.ViewModels;


namespace SaveUpApp.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
        // Connect the UI to the shared ViewModel (used for data binding)
        BindingContext = App.SharedViewModel;
    }
    // Called when the German language button is clicked
    private void OnGermanClicked(object sender, EventArgs e)
    {
        App.ShellViewModel.SetLanguage("de");
    }
    // Called when the English language button is clicked
    private void OnEnglishClicked(object sender, EventArgs e)
    {
        App.ShellViewModel.SetLanguage("en");
    }

    // Called when the user selects a new currency from the dropdown (Picker)
    private void OnCurrencyChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        string selected = picker.SelectedItem?.ToString();

        if (!string.IsNullOrWhiteSpace(selected))
        {
            // Update currency symbol in the ViewModel
            App.ShellViewModel.CurrencySymbol = selected;
            // Refresh currency display on all items
            foreach (var item in App.SharedViewModel.Items)
            {
                item.NotifyCurrencyChanged();
            }

        }
        // Reload the main page to reflect new currency settings
        Application.Current.MainPage = new AppShell();
    }

}