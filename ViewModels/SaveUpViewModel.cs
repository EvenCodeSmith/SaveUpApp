using System.Collections.ObjectModel;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SaveUpApp.Model;

namespace SaveUpApp.ViewModels;

public partial class SaveUpViewModel : ObservableObject
{
    private const string FileName = "saveitems.json";

    [ObservableProperty]
    private ObservableCollection<SaveItem> items = new();

    [ObservableProperty]
    private double totalAmount;

    [ObservableProperty]
    private string newDescription;

    [ObservableProperty]
    private double newAmount;

    public SaveUpViewModel()
    {
        LoadData();
    }

    [RelayCommand]
    private async void AddItem()
    {
        if (string.IsNullOrWhiteSpace(NewDescription))
        {
            await Shell.Current.DisplayAlert("Invalid Input", "Description cannot be empty.", "OK");
            return;
        }


        if (NewAmount <= 0)
        {
            await Shell.Current.DisplayAlert("Invalid Input", "Amount must be greater than 0.", "OK");
            return;
        }

        var item = new SaveItem
        {
            Description = NewDescription,
            Amount = NewAmount,
            SaveDate = DateTime.Now
        };

        Items.Add(item);
        CalculateTotal();
        Save();


        NewDescription = string.Empty;
        NewAmount = 0;

        await Shell.Current.DisplayAlert("Saved", "Entry has been added.", "OK");
    }


    [RelayCommand]
    private void ClearAll()
    {
        Items.Clear();
        CalculateTotal();
        Save();
    }

    private void CalculateTotal()
    {
        TotalAmount = Items.Sum(i => i.Amount);
    }

    private async void Save()
    {
        var path = Path.Combine(FileSystem.AppDataDirectory, FileName);
        var json = JsonSerializer.Serialize(Items);
        await File.WriteAllTextAsync(path, json);
    }

    private async void LoadData()
    {
        var path = Path.Combine(FileSystem.AppDataDirectory, FileName);
        if (File.Exists(path))
        {
            var json = await File.ReadAllTextAsync(path);
            var loadedItems = JsonSerializer.Deserialize<ObservableCollection<SaveItem>>(json);
            if (loadedItems != null)
            {
                Items = loadedItems;
                CalculateTotal();
            }
        }
    }
}
