using DoToo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoToo.ViewModels {
    public class TodoItemViewModel : ViewModel {
        public TodoItem Item { get; private set; }
        public event EventHandler ItemStatusChanged;
        public string StatusText => Item.Completed ? "Reactivate" : "Completed";
        
        public TodoItemViewModel(TodoItem item) => Item = item;
    }
}
