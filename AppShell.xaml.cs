using SaveUpApp.ViewModels;
using System.Linq;
using SaveUpApp.Resources.Languages;


namespace SaveUpApp;

public partial class AppShell : Shell
{
    public ShellViewModel ViewModel => BindingContext as ShellViewModel;


    public AppShell()
    {
        InitializeComponent();
    }

    // Diese Methode rufst du auf, wenn Sprache geändert wird:
    public void UpdateShellTexts()
    {
        ViewModel.UpdateTexts();
    }

    public void UpdateShellTabTitles()
    {
        ViewModel?.UpdateTexts();
    }

}
