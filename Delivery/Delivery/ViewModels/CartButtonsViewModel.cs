using Delivery.Services;
using Delivery.Views;
using MvvmHelpers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Delivery.ViewModels
{

    public class CartButtonsViewModel : BaseViewModel
    {
        private IShoppingCartService shoppingCartService;

        private int _total;

        public int Total
        {
            get { return _total; }
            private set { SetProperty(ref _total, value); }
        }

        public CartButtonsViewModel()
        {
            shoppingCartService = DependencyService.Get<IShoppingCartService>();
            MessagingCenter.Subscribe<ItemViewModel>(this, "CartUpdateFromAddItem", async (upd) =>
            {
                await UpdateTotalAsync();
            });

            MessagingCenter.Subscribe<StoreItemsView>(this, "CartUpdateFromStoreItems", async (upd) =>
            {
                await UpdateTotalAsync();
            });

            UpdateTotalAsync(); // Chame o método para atualizar o total quando o ViewModel for criado
        }

        ~CartButtonsViewModel()
        {
            MessagingCenter.Unsubscribe<CartButtonsViewModel>(this, "CartUpdateFromAddItem");
            MessagingCenter.Unsubscribe<StoreItemsView>(this, "CartUpdateFromStoreItems");
        }

        private async Task UpdateTotalAsync()
        {
            Total = await GetTotal();
        }

        private async Task<int> GetTotal()
        {
            return await shoppingCartService.GetItemCount();
        }
    }
}
