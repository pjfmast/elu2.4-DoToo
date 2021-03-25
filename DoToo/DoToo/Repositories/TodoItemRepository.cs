using DoToo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoToo.Repositories {
    class TodoItemRepository : ITodoItemRepository {
        public event EventHandler<TodoItem> OnItemAdded;
        public event EventHandler<TodoItem> OnItemUpdated;

        public async Task AddItem(TodoItem item) {
        }

        public async Task AddOrUpdateItem(TodoItem item) {
            if (item.Id == 0) {
                await AddItem(item);
            } else {
                await UpdateItem(item);
            }
        }

        public Task DeleteItem(TodoItem item) {
            throw new NotImplementedException();
        }

        public async Task<List<TodoItem>> GetItems() {
            return null;
        }

        public async Task UpdateItem(TodoItem item) {
        }
    }
}
