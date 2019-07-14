using DFMEngine.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFMEngine.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        public AccountModel NewModel { get; set; }

        public ViewModel()
        {
            NewModel = new AccountModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
