using DoToo.Repositories;
using DoToo.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DoToo.ViewModels {
    public class MainViewModel : ViewModel {
        private readonly TodoItemRepository repository;

        public MainViewModel(TodoItemRepository repository) {
            this.repository = repository;
            Task.Run(async () => await LoadData());
        }

        private Task LoadData() {
            throw new NotImplementedException();
        }

        public ICommand AddItem =>
            new Xamarin.Forms.Command(async () => {
                // Autofac builds the necessary dependencies
                var itemView = Resolver.Resolve<ItemView>();
                await Navigation.PushAsync(itemView);
            });
    }
}
