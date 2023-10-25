using Delivery.Models;
using Delivery.Services;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

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
        }

        public async Task UpdateCartItem(ShoppingCartModel item)
        {
            await Init();
            await db.UpdateAsync(item);
        }

        public async Task RemoveCartItem(ShoppingCartModel item)
        {
            await Init();
            await db.DeleteAsync(item);
        }
        public async Task ClearCart()
        {
            await Init();
            await db.DeleteAllAsync<ShoppingCartModel>();
        }
        public async Task<List<ShoppingCartModel>> GetCartList()
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

        public async Task<int> GetCount()
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

        public async Task<int> GetTotalQuantityItems()
        {
            await Init();
            int totalQuantity = await db.ExecuteScalarAsync<int>("SELECT SUM(Quantity) FROM ShoppingCartModel");
            return totalQuantity;
        }

        public async Task<int> GetStoreId()
        {
            if (CurrentStoreId == 0)
            {
                await Init();
                var item = await db.Table<ShoppingCartModel>().FirstOrDefaultAsync(c => c.IdStore > 0);
                if (item != null)
                    CurrentStoreId = item.IdStore;
            }

            return CurrentStoreId;
        }
    }
}
