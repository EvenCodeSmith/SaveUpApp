using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveUpApp.Resources.Languages;
using CommunityToolkit.Mvvm.ComponentModel;
using SaveUpApp.Resources.Languages;
using System.Globalization;

namespace SaveUpApp.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    // Tab title: Overview
    [ObservableProperty] private string tabOverview = SaveUpApp.Resources.Languages.Resources.TabOverview;
    // Tab title: Add Savings
    [ObservableProperty] private string tabAddSavings = SaveUpApp.Resources.Languages.Resources.TabAddSavings;
    // Tab title: List
    [ObservableProperty] private string tabList = SaveUpApp.Resources.Languages.Resources.TabList;
    // Tab title: Settings
    [ObservableProperty] private string tabSettings = SaveUpApp.Resources.Languages.Resources.TabSettings;
    // Currency symbol used in the app (e.g. CHF or €)
    [ObservableProperty] private string currencySymbol = "CHF";

    // Updates the localized tab titles
    public void UpdateTexts()
    {
        TabOverview = SaveUpApp.Resources.Languages.Resources.TabOverview;
        TabAddSavings = SaveUpApp.Resources.Languages.Resources.TabAddSavings;
        TabList = SaveUpApp.Resources.Languages.Resources.TabList;
    }
    // Changes the language of the application
    public void SetLanguage(string cultureCode)
    {
        try
        {
            var culture = new CultureInfo(cultureCode);
            // Set culture for current and future threads
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            // Apply to resx system
            SaveUpApp.Resources.Languages.Resources.Culture = culture;

            // Update all observable text bindings
            UpdateTexts(); // for tab titles
            Application.Current.MainPage = new AppShell();
            App.ShellViewModel.NotifyAllItemsChanged(); // for item date labels
            

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[Localization] Failed to switch to {cultureCode}: {ex.Message}");
        }
    }
    // Notifies all items to update their displayed date values
    public void NotifyAllItemsChanged()
    {
        foreach (var item in App.SharedViewModel.Items)
        {
            item.NotifyDateChanged();
        }
    }

    




}
