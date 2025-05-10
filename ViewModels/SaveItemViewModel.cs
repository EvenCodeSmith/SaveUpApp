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
    public class SaveItemViewModel
    {
        public SaveItem Model { get; }
        private SaveUpViewModel _parent;

        public string Description => Model.Description;
        public double Amount => Model.Amount;
        public DateTime SaveDate => Model.SaveDate;

        public ICommand DeleteCommand { get; }

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
    }

}
