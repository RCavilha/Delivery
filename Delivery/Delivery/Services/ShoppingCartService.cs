using Delivery.Models;
using Delivery.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

[assembly: Dependency(typeof(ShoppingCartService))]
namespace Delivery.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        public int CurrentStoreId { get; private set; }

        SQLiteAsyncConnection db;
        async Task Init()
        {
            if (db != null)
                return;

            CurrentStoreId = 0;

            // Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "22CartData.db");//9

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<ShoppingCartModel>();
        }

        public async Task AddItemToCart(int idStore, int idItem, string image, string name, double price, int quantity)
        {
            CurrentStoreId = idStore;

            await Init();

            var item = new ShoppingCartModel
            {
                IdStore = idStore,
                IdItem = idItem,
                Image = image,
                Name = name,
                UnitPrice = price,
                Quantity = quantity,
                TotalPrice = price * quantity,
            };

            var exist = await GetCartItem(idItem);

            if ((exist == null) || (exist.IdItem != idItem))
            {
                await db.InsertAsync(item);
            }
            else
            {
                await db.UpdateAsync(item);
            }

            MessagingCenter.Send(this, "CartUpdate");
        }

        public async Task UpdateCartItem(ShoppingCartModel item)
        {
            await Init();
            await db.UpdateAsync(item);
            MessagingCenter.Send(this, "CartUpdate");
        }

        public async Task RemoveCartItem(ShoppingCartModel item)
        {
            await Init();
            await db.DeleteAsync(item);
            MessagingCenter.Send(this, "CartUpdate");
        }

        public async Task<List<ShoppingCartModel>> GetCartItem()
        {
            await Init();

            var listItem = await db.Table<ShoppingCartModel>().ToListAsync();            
            return listItem;
        }

        public async Task<ShoppingCartModel> GetCartItem(int idItem)
        {
            await Init();

            var item = await db.Table<ShoppingCartModel>().FirstOrDefaultAsync(c => c.IdItem == idItem);

            return item;
        }

        public async Task<int> GetItemCount()
        {
            await Init();
            return await db.Table<ShoppingCartModel>().CountAsync();
        }

        public async Task<double> GetTotalPrice()
        {
            await Init();
            double total = await db.ExecuteScalarAsync<double>("SELECT SUM(TotalPrice) FROM ShoppingCartModel");
            return total;
        }

        public async Task<bool> StoreChanged(int idStoreSelected)
        {
            if (CurrentStoreId > 0)
            {
                return CurrentStoreId == idStoreSelected;
            }

            await Init();
            var exist = await db.Table<ShoppingCartModel>().FirstOrDefaultAsync(c => c.IdStore == idStoreSelected);

            return exist == null;
        }

    }
}
