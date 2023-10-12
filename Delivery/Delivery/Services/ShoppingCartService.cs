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
        SQLiteAsyncConnection db;
        async Task Init()
        {
            if (db != null)
                return;

            // Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "6CartData.db");

            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<ShoppingCartModel>();
        }

        public async Task AddItemToCart(int idStore, int idItem, string image, string name, double price, int quantity)
        {
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

        public async Task RemoveCartItem(ShoppingCartModel item)
        {
            await Init();
            await db.DeleteAsync(item);
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
            int itemCount = await GetItemCount();

            if (itemCount == 0)
            {
                return false;
            }

            var exist = await db.Table<ShoppingCartModel>().FirstOrDefaultAsync(c => c.IdStore == idStoreSelected);

            if (exist == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
