using Delivery.Libraries.Helpers.MVVM;
using Delivery.Models;
using Delivery.Services;
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace Delivery.ViewModels
{
    public class OrderCompletionViewModel : BaseViewModel
    {
        private OrderModel _order;
        private bool _seending = false;
        private bool _isSent = false;
        private IShoppingCartService _shoppingCartService;
        private IOrderService _orderService;
        private IStoreService _storeService;

        public OrderCompletionViewModel()
        {
            _shoppingCartService = DependencyService.Get<IShoppingCartService>();
            _orderService = DependencyService.Get<IOrderService>();
            _storeService = DependencyService.Get<IStoreService>();
            SendOrderCommand = new Command(SendOrder);
            OkSentCommand = new AsyncCommand(async () => { await Shell.Current.Navigation.PopToRootAsync(); });
            SetCurrentOrder();
        }

        public ICommand SendOrderCommand { get; set; }
        public AsyncCommand OkSentCommand { get; set; }

        public OrderModel Order
        {
            get { return _order; }
            set { SetProperty(ref _order, value); }
        }

        public bool Seending
        {
            get { return _seending; }
            set { SetProperty(ref _seending, value); }
        }

        public bool IsSent
        {
            get { return _isSent; }
            set { SetProperty(ref _isSent, value); }
        }

        private async void SetCurrentOrder()
        {
            Order = new OrderModel();
            Order.UserLogin = "admin";
            Order.StoreId = await _shoppingCartService.GetStoreId();
            Order.StoreName = await _storeService.GetStoreName(Order.StoreId);
            Order.DeliveryType = "Entrega";
            Order.PaymentType = "Dinheiro";
            Order.TotalQuantity = await _shoppingCartService.GetTotalQuantityItems();
            Order.TotalPrice = await _shoppingCartService.GetTotalPrice();
            Order.DiscountValue = 0;
            Order.TotalAmountToPay = Order.TotalPrice - Order.DiscountValue;
            OnPropertyChanged(nameof(Order));
        }

        public async void SendOrder()
        {
            if (Seending)
                return;

            Seending = true;
            await IncludeFinalOrderData();
            MessagingCenter.Send(Order, "AddOrderToHistory");
            await _orderService.AddUserOrder(Order);
            await _shoppingCartService.ClearCart();
            await Task.Delay(1800);
            IsSent = true;
        }

        private async Task IncludeFinalOrderData()
        {
            var orderCount = await IncrementOrderCount();
            Order.OrderId = Order.StoreId.ToString("D4") + orderCount.ToString("D4");
            Order.OrderDateTime = DateTime.Now;
            Order.ShoppingCart = await _shoppingCartService.GetCartList();
        }

        private async Task<int> IncrementOrderCount()
        {
            var orderCount = await _orderService.GetOrderCount();
            orderCount++;
            return orderCount;
        }
    }
}
