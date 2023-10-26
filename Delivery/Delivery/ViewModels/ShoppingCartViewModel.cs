using Delivery.Libraries.Helpers.MVVM;
using Delivery.Models;
using Delivery.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Delivery.ViewModels
{
    public class ShoppingCartViewModel : BaseViewModel
    {
        private double _shoppingCartTotalPrice;
        private IShoppingCartService _shoppingCartService;
        private ObservableCollection<ShoppingCartModel> _cartList;

        public ShoppingCartViewModel()
        {
            _shoppingCartService = DependencyService.Get<IShoppingCartService>();
            FinishCommand = new Command(GoToFinish);
            IncSelectedItemCountCommand = new Command<ShoppingCartModel>(IncSelectedItemCount);
            DecSelectedItemCountCommand = new Command<ShoppingCartModel>(DecSelectedItemCount);
            GetCartList();
        }

        public StoreModel Store { get; set; }
        public ICommand FinishCommand { get; set; }
        public ICommand IncSelectedItemCountCommand { get; set; }
        public ICommand DecSelectedItemCountCommand { get; set; }

        public double ShoppingCartTotalPrice
        {
            get { return _shoppingCartTotalPrice; }
            set { SetProperty(ref _shoppingCartTotalPrice, value); }
        }

        public ObservableCollection<ShoppingCartModel> CartList
        {
            get { return _cartList; }
            set { SetProperty(ref _cartList, value); }
        }

        public async void GetCartList()
        {
            var itemList = await _shoppingCartService.GetCartList();
            CartList = new ObservableCollection<ShoppingCartModel>(itemList);
            TotalPrice();
        }
        public void GoToFinish()
        {
            Shell.Current.GoToAsync($"cart/ordercompletion");
        }
        public async void DecSelectedItemCount(ShoppingCartModel item)
        {

            if (item.Quantity <= 1)
            {
                await _shoppingCartService.RemoveCartItem(item);
                CartList.Remove(item);
                TotalPrice();                
            }
            else
            {
                item.Quantity--;
                item.TotalPrice = item.UnitPrice * item.Quantity;
                TotalPrice();
                await _shoppingCartService.UpdateCartItem(item);
                OnPropertyChanged(nameof(item));
            }
        }
        public async void IncSelectedItemCount(ShoppingCartModel item)
        {
            item.Quantity++;
            item.TotalPrice = item.UnitPrice * item.Quantity;
            TotalPrice();
            await _shoppingCartService.UpdateCartItem(item);
            OnPropertyChanged(nameof(item));
        }
        public void TotalPrice()
        {
            double soma = 0;
            foreach (var item in CartList)
            {
                soma += item.TotalPrice;
            }
            ShoppingCartTotalPrice = soma;
        }
    }
}
