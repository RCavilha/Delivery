using Delivery.Libraries.Helpers.MVVM;
using Delivery.Models;
using Delivery.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace Delivery.ViewModels
{
    public class OrderCompletionViewModel : BaseViewModel
    {
        private OrderModel _order;
        public OrderModel Order
        {
            get
            {
                return _order;
            }
            set
            {
                SetProperty(ref _order, value);
            }
        }

        private bool _seending = false;

        public bool Seending
        {
            get
            {
                return _seending;
            }
            set
            {
                SetProperty(ref _seending, value);
            }
        }


        IShoppingCartService shoppingCartService;
        IOrderService orderService;
        public ICommand SendOrderCommand { get; set; }
        public OrderCompletionViewModel()
        {
            shoppingCartService = DependencyService.Get<IShoppingCartService>();
            orderService = DependencyService.Get<IOrderService>();
            SendOrderCommand = new Command(SendOrder);
            _getOrder();
        }

        private async void _getOrder()
        {
            Order = new OrderModel();
            Order.UserLogin = "admin";
            Order.StoreId = shoppingCartService.GetStoreId();
            Order.DeliveryType = "Entrega";
            Order.PaymentType = "Dinheiro";
            Order.TotalQuantity = await shoppingCartService.GetTotalQuantityItems();
            Order.TotalPrice = await shoppingCartService.GetTotalPrice();
            Order.DiscountValue = 0;
            Order.TotalAmountToPay = Order.TotalPrice - Order.DiscountValue;
            OnPropertyChanged(nameof(Order));
        }
        public async void SendOrder()
        {
            Seending = true;
            int orderCount = await orderService.GetOrderCount();
            orderCount++;
            Order.OrderId = Order.StoreId.ToString("D4") + orderCount.ToString("D4");
            Order.OrderDateTime = DateTime.Now;            
            Order.ShoppingCart = await shoppingCartService.GetCartList();
            MessagingCenter.Send(Order, "AddOrderToHistory");
            await orderService.AddUserOrder(Order);
            await shoppingCartService.ClearCart();
            await Shell.Current.Navigation.PopToRootAsync();
        }
    }
}
