// SaveUpViewModel.cs
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
    private double totalAmount;

    [ObservableProperty]
    private string newDescription;

    [ObservableProperty]
    private double newAmount;

    public ObservableCollection<SaveItemViewModel> Items { get; set; } = new();

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

        await Shell.Current.DisplayAlert("Saved", "Entry has been added.", "OK");
    }

    [RelayCommand]
    private void ClearAll()
    {
        Items.Clear();
        CalculateTotal();
        Save();
    }

    public void DeleteItem(SaveItem item)
    {
        var toRemove = Items.FirstOrDefault(i => i.Model == item);
        if (toRemove != null)
        {
            Items.Remove(toRemove);
            CalculateTotal();
            Save();
        }
    }

    private void CalculateTotal()
    {
        TotalAmount = Items.Sum(i => i.Model.Amount);
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
}
