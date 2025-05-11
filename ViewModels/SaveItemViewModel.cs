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
    public class SaveItemViewModel : ObservableObject
    {
        public SaveItem Model { get; }
        private SaveUpViewModel _parent;

        public string Description => Model.Description;
        public double Amount => Model.Amount;
        public DateTime SaveDate => Model.SaveDate;

        public ICommand DeleteCommand { get; }

        public string LocalizedSaveDate =>
            string.Format(Resources.Languages.Resources.SavedOnText, Model.SaveDate.ToString("dd.MM.yyyy HH:mm"));


        public SaveItemViewModel(SaveItem model, SaveUpViewModel parent)
        {
            Model = model;
            _parent = parent;
            DeleteCommand = new RelayCommand(OnDelete);
        }

        private void OnDelete()
        {
            _parent.DeleteItem(Model);
        }

        public void NotifyDateChanged()
        {
            OnPropertyChanged(nameof(LocalizedSaveDate));
        }
    }

}
