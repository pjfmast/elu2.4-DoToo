using DoToo.Models;
using DoToo.Repositories;
using DoToo.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DoToo.ViewModels {
    public class MainViewModel : ViewModel {
        private readonly TodoItemRepository repository;

        public ObservableCollection<TodoItemViewModel> Items { get; set; }

        public bool ShowAll { get; set; }

        public string FilterText => ShowAll ? "All" : "Active";

        public ICommand ToggleFilter => new Command(async () => {
            ShowAll = !ShowAll;
            await LoadData();
        });

        public MainViewModel(TodoItemRepository repository) {

            repository.OnItemAdded += (sender, item) =>
                Items.Add(CreateTodoItemViewModel(item));
            repository.OnItemUpdated += (sender, item) =>
                Task.Run(async () => await LoadData());
            this.repository = repository;
            Task.Run(async () => await LoadData());
        }


        public TodoItemViewModel SelectedItem {
            get { return null; }
            set { 
                Device.BeginInvokeOnMainThread(async () => NavigateToItem(value));
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        private async void NavigateToItem(TodoItemViewModel item) {
            if (item == null) {
                return;
            }

            var itemView = Resolver.Resolve<ItemView>();
            var vm = itemView.BindingContext as ItemViewModel;
            vm.Item = item.Item;

            await Navigation.PushAsync(itemView);
        }

        private async Task LoadData() {
            var items = await repository.GetItems();
            // page 89, Creating the filter toggle function using a command
            if (!ShowAll) {
                items = items.Where(x => x.Completed == false).ToList();
            }

            var itemViewModels = items.Select(
                i => CreateTodoItemViewModel(i));
            Items = new ObservableCollection<TodoItemViewModel>(itemViewModels);
        }

        private TodoItemViewModel CreateTodoItemViewModel(TodoItem item) {
            var itemViewModel = new TodoItemViewModel(item);
            itemViewModel.ItemStatusChanged += ItemStatusChanged;
            return itemViewModel;
        }

        private void ItemStatusChanged(object sender, EventArgs e) {
            if (sender is TodoItemViewModel item) {
                if (!ShowAll && item.Item.Completed) {
                    Items.Remove(item);
                }
                Task.Run(async () => await repository.UpdateItem(item.Item));
            }
        }

        public ICommand AddItem =>
            new Xamarin.Forms.Command(async () => {
                // Autofac builds the necessary dependencies
                var itemView = Resolver.Resolve<ItemView>();
                await Navigation.PushAsync(itemView);
            });
    }
}
