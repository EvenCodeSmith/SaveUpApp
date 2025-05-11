using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveUpApp.Resources.Languages;
using CommunityToolkit.Mvvm.ComponentModel;
using SaveUpApp.Resources.Languages;

namespace SaveUpApp.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty] private string tabOverview = SaveUpApp.Resources.Languages.Resources.TabOverview;
    [ObservableProperty] private string tabAddSavings = SaveUpApp.Resources.Languages.Resources.TabAddSavings;
    [ObservableProperty] private string tabList = SaveUpApp.Resources.Languages.Resources.TabList;

    public void UpdateTexts()
    {
        TabOverview = SaveUpApp.Resources.Languages.Resources.TabOverview;
        TabAddSavings = SaveUpApp.Resources.Languages.Resources.TabAddSavings;
        TabList = SaveUpApp.Resources.Languages.Resources.TabList;
    }
}
