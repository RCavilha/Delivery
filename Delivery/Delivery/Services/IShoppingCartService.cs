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
        Task<List<ShoppingCartModel>> GetCartItem();
        Task<ShoppingCartModel> GetCartItem(int idItem);
        Task<int> GetItemCount();
        Task<double> GetTotalPrice();
        Task<bool> StoreChanged(int idStoreSelected);

    }
}
