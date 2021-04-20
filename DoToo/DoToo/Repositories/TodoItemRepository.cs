﻿using DoToo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;

namespace DoToo.Repositories {
    public class TodoItemRepository : ITodoItemRepository {
        private SQLiteAsyncConnection connection;

        public event EventHandler<TodoItem> OnItemAdded;
        public event EventHandler<TodoItem> OnItemDeleted;
        public event EventHandler<TodoItem> OnItemUpdated;

        private async Task CreateConnection() {
            if (connection != null) {
                return;
            }
            // In this case, we will choose the MyDocuments folder.
            // Xamarin will find the closest match to this on each platform that we target.
            //var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //var databasePath = Path.Combine(documentPath, "TodoItems.db");

            //connection = new SQLiteAsyncConnection(databasePath);
            connection = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await connection.CreateTableAsync<TodoItem>();

            if (await connection.Table<TodoItem>().CountAsync() == 0) {
                await connection.InsertAsync(new TodoItem() {
                    Title = "Welcome to DoToo",
                    Due = DateTime.Now
                });
            }
        }
        public async Task AddItem(TodoItem item) {
            await CreateConnection();
            await connection.InsertAsync(item);

            // After an item has been inserted into the table,
            // we invoke the OnItemAdded event to notify any subscribers
            OnItemAdded?.Invoke(this, item);
        }

        public async Task AddOrUpdateItem(TodoItem item) {
            if (item.Id == 0) {
                await AddItem(item);
            } else {
                await UpdateItem(item);
            }
        }

        public async Task DeleteItem(TodoItem item) {
            //throw new NotImplementedException();
            await CreateConnection();
            await connection.DeleteAsync(item);

            // After an item has been deleted from the table,
            // we invoke the OnItemDeleted event to notify any subscribers
            OnItemDeleted?.Invoke(this, item);
        }

        public async Task<List<TodoItem>> GetItems() {
            await CreateConnection();
            return await connection
                .Table<TodoItem>()
                .ToListAsync();
        }

        public async Task UpdateItem(TodoItem item) {
            await CreateConnection();
            await connection.UpdateAsync(item);
            OnItemUpdated?.Invoke(this, item);
        }
    }
}
