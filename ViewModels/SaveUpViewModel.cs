// SaveUpViewModel.cs
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SaveUpApp.Model;
using Resources = SaveUpApp.Resources.Languages.Resources;

namespace SaveUpApp.ViewModels;

public partial class SaveUpViewModel : ObservableObject
{

    // Filename for saving local data
    private const string FileName = "saveitems.json";

    // Total amount of all saved entries
    [ObservableProperty]
    private double totalAmount;

    // Description for a new entry
    [ObservableProperty]
    private string newDescription;

    // Amount for a new entry
    [ObservableProperty]
    private double newAmount;

    // Text for the clear button
    [ObservableProperty]
    private string clearButtonText = SaveUpApp.Resources.Languages.Resources.ClearButton;

    // Text label for "Saved Items"
    [ObservableProperty]
    private string savedItemsText = SaveUpApp.Resources.Languages.Resources.SavedItems;

    // Label for language selection
    [ObservableProperty]
    private string languageLabel = SaveUpApp.Resources.Languages.Resources.LanguageLabel;

    // Total amount formatted with currency
    public string LocalizedTotalAmount =>
        $"{App.ShellViewModel.CurrencySymbol} {TotalAmount:F2}";


    // Collection of all saved entries
    public ObservableCollection<SaveItemViewModel> Items { get; set; } = new();

    // Constructor loads saved data and resets input
    public SaveUpViewModel()
    {
        LoadData();
        NewDescription = string.Empty;
    }


    // Command: Add a new entry
    [RelayCommand]
    private async void AddItem()
    {
        // Input validation
        if (string.IsNullOrWhiteSpace(NewDescription))
        {
            await Shell.Current.DisplayAlert(Resources.Languages.Resources.InvalidInputTitle, Resources.Languages.Resources.EmptyDescription, Resources.Languages.Resources.OkButton);
            return;
        }

        if (NewAmount <= 0)
        {
            await Shell.Current.DisplayAlert(Resources.Languages.Resources.InvalidInputTitle, Resources.Languages.Resources.GreaterThanZero, Resources.Languages.Resources.OkButton);
            return;
        }

        if (NewDescription.Length > 20)
        {
            await Shell.Current.DisplayAlert(Resources.Languages.Resources.InvalidInputTitle, Resources.Languages.Resources.DescriptionError, Resources.Languages.Resources.OkButton);
            return;
        }
        if (NewAmount > 10000)
        {
            await Shell.Current.DisplayAlert(Resources.Languages.Resources.InvalidInputTitle, Resources.Languages.Resources.AmountError, Resources.Languages.Resources.OkButton);
            return;
        }
        // Create new entry
        var model = new SaveItem
        {
            Description = NewDescription,
            Amount = NewAmount,
            SaveDate = DateTime.Now
        };


        // Add to list and reset input
        Items.Add(new SaveItemViewModel(model, this));

        NewDescription = string.Empty;
        NewAmount = 0;

        CalculateTotal();
        Save();

        // Show confirmation message
        await Shell.Current.DisplayAlert(Resources.Languages.Resources.EntrySavedTitle,
                                         Resources.Languages.Resources.EntrySavedMessage,
                                         Resources.Languages.Resources.OkButton
                                         );
    }

    // Command: Clear all entries
    [RelayCommand]
    private void ClearAll()
    {
        Items.Clear();
        CalculateTotal();
        Save();
    }

    // Deletes an item after confirmation
    public async Task DeleteItem(SaveItem item)
    {
        var toRemove = Items.FirstOrDefault(i => i.Model == item);

        bool confirm = await Shell.Current.DisplayAlert(Resources.Languages.Resources.DeleteTitle, Resources.Languages.Resources.DeletionText, Resources.Languages.Resources.YesButton, Resources.Languages.Resources.NoButton);

        if (confirm)
        {
            if (toRemove != null)
            {
                Items.Remove(toRemove);
                CalculateTotal();
                Save();
            }
        }
    }
    // Calculates the total amount
    private void CalculateTotal()
    {
        TotalAmount = Items.Sum(i => i.Model.Amount);
        OnPropertyChanged(nameof(LocalizedTotalAmount));
    }

    private async void Save()
    {
        var path = Path.Combine(FileSystem.AppDataDirectory, FileName);
        var models = Items.Select(i => i.Model).ToList();
        var json = JsonSerializer.Serialize(models);
        await File.WriteAllTextAsync(path, json);
    }

    // Saves all data to a local JSON file
    private async void LoadData()
    {
        var path = Path.Combine(FileSystem.AppDataDirectory, FileName);
        if (File.Exists(path))
        {
            var json = await File.ReadAllTextAsync(path);
            var loadedItems = JsonSerializer.Deserialize<List<SaveItem>>(json);
            if (loadedItems != null)
            {
                Items = new ObservableCollection<SaveItemViewModel>(
                    loadedItems.Select(model => new SaveItemViewModel(model, this)));
                CalculateTotal();
            }
        }
    }

    // Command: Switch language to German
    [RelayCommand]
    private void SwitchToGerman()
    {
        App.ChangeLanguage("de");
        (App.Current.MainPage as AppShell)?.UpdateShellTexts();
        (App.Current.MainPage as AppShell)?.UpdateShellTabTitles();
        UpdateTexts();
    }

    // Command: Switch language to English
    [RelayCommand]
    private void SwitchToEnglish()
    {
        App.ChangeLanguage("en");
        (App.Current.MainPage as AppShell)?.UpdateShellTexts();
        (App.Current.MainPage as AppShell)?.UpdateShellTabTitles();
        UpdateTexts();
    }

    // Updates localized texts after language switch
    private void UpdateTexts()
    {
        ClearButtonText = SaveUpApp.Resources.Languages.Resources.ClearButton;
        SavedItemsText = SaveUpApp.Resources.Languages.Resources.SavedItems;
    }

   

}



