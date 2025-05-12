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
    private const string FileName = "saveitems.json";

    [ObservableProperty]
    private double totalAmount;

    [ObservableProperty]
    private string newDescription;

    [ObservableProperty]
    private double newAmount;

    [ObservableProperty]
    private string clearButtonText = SaveUpApp.Resources.Languages.Resources.ClearButton;


    [ObservableProperty]
    private string savedItemsText = SaveUpApp.Resources.Languages.Resources.SavedItems;

    [ObservableProperty]
    private string languageLabel = SaveUpApp.Resources.Languages.Resources.LanguageLabel;

    public string LocalizedTotalAmount =>
        $"{App.ShellViewModel.CurrencySymbol} {TotalAmount:F2}";





    public ObservableCollection<SaveItemViewModel> Items { get; set; } = new();

    public SaveUpViewModel()
    {
        LoadData();
        NewDescription = string.Empty;
    }

    [RelayCommand]
    private async void AddItem()
    {
        
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

        var model = new SaveItem
        {
            Description = NewDescription,
            Amount = NewAmount,
            SaveDate = DateTime.Now
        };

        

        Items.Add(new SaveItemViewModel(model, this));

        NewDescription = string.Empty;
        NewAmount = 0;

        CalculateTotal();
        Save();

        await Shell.Current.DisplayAlert(Resources.Languages.Resources.EntrySavedTitle,
                                         Resources.Languages.Resources.EntrySavedMessage,
                                         Resources.Languages.Resources.OkButton
                                         );
    }

    [RelayCommand]
    private void ClearAll()
    {
        Items.Clear();
        CalculateTotal();
        Save();
    }

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


    [RelayCommand]
    private void SwitchToGerman()
    {
        App.ChangeLanguage("de");
        (App.Current.MainPage as AppShell)?.UpdateShellTexts();
        (App.Current.MainPage as AppShell)?.UpdateShellTabTitles();
        UpdateTexts();
    }

    [RelayCommand]
    private void SwitchToEnglish()
    {
        App.ChangeLanguage("en");
        (App.Current.MainPage as AppShell)?.UpdateShellTexts();
        (App.Current.MainPage as AppShell)?.UpdateShellTabTitles();
        UpdateTexts();
    }



    private void UpdateTexts()
    {
        ClearButtonText = SaveUpApp.Resources.Languages.Resources.ClearButton;
        SavedItemsText = SaveUpApp.Resources.Languages.Resources.SavedItems;
    }

   

}



