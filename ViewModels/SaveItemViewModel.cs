using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SaveUpApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SaveUpApp.ViewModels
{
    // ViewModel for a single saved item (entry)
    public class SaveItemViewModel : ObservableObject
    {

        // The data model of the item
        public SaveItem Model { get; }
        private SaveUpViewModel _parent;

        // Description of the item
        public string Description => Model.Description;

        // Amount of the saved item
        public double Amount => Model.Amount;

        // Amount with currency symbol (e.g., CHF 5.00)
        public string LocalizedAmount =>
                        $"{App.ShellViewModel.CurrencySymbol} {Model.Amount:F2}";

        // Date when the item was saved
        public DateTime SaveDate => Model.SaveDate;

        // Command to delete this item
        public ICommand DeleteCommand { get; }


        // Localized string showing when the item was saved
        public string LocalizedSaveDate =>
            string.Format(Resources.Languages.Resources.SavedOnText, Model.SaveDate.ToString("dd.MM.yyyy HH:mm"));

        // Constructor initializes the model and delete command
        public SaveItemViewModel(SaveItem model, SaveUpViewModel parent)
        {
            Model = model;
            _parent = parent;
            DeleteCommand = new RelayCommand(OnDelete);
        }
        // Called when the delete command is executed
        private void OnDelete()
        {
            _parent.DeleteItem(Model);
        }
        // Notifies that the date changed (UI updates the display)
        public void NotifyDateChanged()
        {
            OnPropertyChanged(nameof(LocalizedSaveDate));
        }
        // Notifies that the currency format changed
        public void NotifyCurrencyChanged()
        {
            OnPropertyChanged(nameof(App.SharedViewModel.LocalizedTotalAmount));
        }

    }

}
