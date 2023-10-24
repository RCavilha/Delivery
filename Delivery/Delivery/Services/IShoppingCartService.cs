using Delivery.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Delivery.Services
{
    public interface IShoppingCartService
    {
        Task AddItemToCart(int idStore, int idItem, string image, string name, double price, int quantity);
        Task UpdateCartItem(ShoppingCartModel item);
        Task RemoveCartItem(ShoppingCartModel item);
        Task ClearCart();
        Task<List<ShoppingCartModel>> GetCartList();
        Task<ShoppingCartModel> GetCartItem(int idItem);
        Task<int> GetCount();
        Task<double> GetTotalPrice();
        Task<bool> StoreChanged(int idStoreSelected);
        Task<int> GetTotalQuantityItems();
        int GetStoreId();
    }
}
