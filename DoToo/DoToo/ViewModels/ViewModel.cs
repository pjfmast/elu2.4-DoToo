using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace DoToo.ViewModels {
    public abstract class ViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        // Our ViewModel class must raise this event
        // whenever we want the GUI to be aware of any changes to a property.
        public void RaisePropertyChanged(params string[] propertyNames) {
            foreach (var propertyName in propertyNames) {
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

        public INavigation Navigation { get; set; }
    }
}
