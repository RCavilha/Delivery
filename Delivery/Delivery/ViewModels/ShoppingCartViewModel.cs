using Delivery.Libraries.Helpers.MVVM;
using Delivery.Models;
using Delivery.Services;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Delivery.ViewModels
{
    public class ShoppingCartViewModel : BaseViewModel
    {
        private List<ShoppingCartModel> _cartList;
        public List<ShoppingCartModel> CartList
        {
            get
            {
                return _cartList;
            }
            set
            {
                SetProperty(ref _cartList, value);
            }
        }
        IShoppingCartService shoppingCartService;

        public ShoppingCartViewModel()
        {
            shoppingCartService = DependencyService.Get<IShoppingCartService>();
            GetCartList();
        }

        public async void GetCartList()
        {
            CartList = await shoppingCartService.GetCartItem();
        }
    }
}
