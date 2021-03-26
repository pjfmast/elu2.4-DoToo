using DoToo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DoToo.Repositories {
    public interface ITodoItemRepository {
        event EventHandler<TodoItem> OnItemAdded;
        event EventHandler<TodoItem> OnItemDeleted;
        event EventHandler<TodoItem> OnItemUpdated;

        Task<List<TodoItem>> GetItems();
        Task AddItem(TodoItem item);
        Task UpdateItem(TodoItem item);
        Task AddOrUpdateItem(TodoItem item);

        // Not in the book xamarin forms projects:
        Task DeleteItem(TodoItem item);
    }
}
