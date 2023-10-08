using Delivery.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Services
{
    public interface IShoppingCartService
    {

        Task AddItemToCart(int idStore, int idItem, string image, string name, double price, int quantity);
        Task RemoveCartItem(ShoppingCartModel item);
        Task<List<ShoppingCartModel>> GetCartItem();
        Task<ShoppingCartModel> GetCartItem(int idItem);
        Task<int> GetItemCount();
        Task<bool> StoreChanged(int idStoreSelected);

    }
}
