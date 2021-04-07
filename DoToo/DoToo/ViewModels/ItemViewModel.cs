using DoToo.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoToo.ViewModels {
    public class ItemViewModel : ViewModel {
        private readonly TodoItemRepository repository;

        public ItemViewModel(TodoItemRepository repository) {
            this.repository = repository;
        }
    }
}
