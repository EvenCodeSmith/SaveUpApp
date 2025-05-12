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
    [ObservableProperty] private string tabOverview = SaveUpApp.Resources.Languages.Resources.TabOverview;
    [ObservableProperty] private string tabAddSavings = SaveUpApp.Resources.Languages.Resources.TabAddSavings;
    [ObservableProperty] private string tabList = SaveUpApp.Resources.Languages.Resources.TabList;
    [ObservableProperty] private string tabSettings = SaveUpApp.Resources.Languages.Resources.TabSettings;
    [ObservableProperty] private string currencySymbol = "CHF";

    public void UpdateTexts()
    {
        TabOverview = SaveUpApp.Resources.Languages.Resources.TabOverview;
        TabAddSavings = SaveUpApp.Resources.Languages.Resources.TabAddSavings;
        TabList = SaveUpApp.Resources.Languages.Resources.TabList;
    }

    public void SetLanguage(string cultureCode)
    {
        try
        {
            var culture = new CultureInfo(cultureCode);

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
    public void NotifyAllItemsChanged()
    {
        foreach (var item in App.SharedViewModel.Items)
        {
            item.NotifyDateChanged();
        }
    }

    




}
