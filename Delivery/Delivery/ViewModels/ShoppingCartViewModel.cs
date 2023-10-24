using Delivery.Libraries.Helpers.MVVM;
using Delivery.Models;
using Delivery.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Delivery.ViewModels
{
    public class ShoppingCartViewModel : BaseViewModel
    {
        public StoreModel Store { get; set; }

        private double _shoppingCartTotalPrice;

        public double ShoppingCartTotalPrice
        {
            get 
            { 
                return _shoppingCartTotalPrice; 
            }
            set 
            { 
                SetProperty(ref _shoppingCartTotalPrice, value);
            }
        }

        private ObservableCollection<ShoppingCartModel> _cartList;
        public ObservableCollection<ShoppingCartModel> CartList
        {
            get { return _cartList; }
            set { SetProperty(ref _cartList, value); }
        }
        public ICommand FinishCommand { get; set; }
        public ICommand IncSelectedItemCountCommand { get; set; }
        public ICommand DecSelectedItemCountCommand { get; set; }
        
        IShoppingCartService shoppingCartService;

        public ShoppingCartViewModel()
        {
            shoppingCartService = DependencyService.Get<IShoppingCartService>();
            FinishCommand = new Command(GoToFinish);
            IncSelectedItemCountCommand = new Command<ShoppingCartModel>(IncSelectedItemCount);
            DecSelectedItemCountCommand = new Command<ShoppingCartModel>(DecSelectedItemCount);
            GetCartList();
        }
        public async void GetCartList()
        {
           var itemList = await shoppingCartService.GetCartList();
           CartList = new ObservableCollection<ShoppingCartModel>(itemList);
           TotalPrice();
        }
        public void GoToFinish()
        {
            Shell.Current.GoToAsync($"cart/ordercompletion");
        }
        public async void DecSelectedItemCount(ShoppingCartModel item)
        {            

            if(item.Quantity <= 1)
            {
                await shoppingCartService.RemoveCartItem(item);
                CartList.Remove(item);
                TotalPrice();
                //OnPropertyChanged(nameof(CartList));
            }
            else
            {
                item.Quantity--;
                item.TotalPrice = item.UnitPrice * item.Quantity;
                TotalPrice();
                await shoppingCartService.UpdateCartItem(item);
            }
        }
        public async void IncSelectedItemCount(ShoppingCartModel item)
        {
            item.Quantity++;   
            item.TotalPrice = item.UnitPrice * item.Quantity;
            TotalPrice();
            await shoppingCartService.UpdateCartItem(item);            
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
