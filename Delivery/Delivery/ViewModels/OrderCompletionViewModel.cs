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
        public OrderModel Order
        {
            get { return _order; }
            set { SetProperty(ref _order, value); }
        }

        private bool _seending = false;
        public bool Seending
        {
            get { return _seending; }
            set { SetProperty(ref _seending, value); }
        }

        private bool _isSent = false;

        public bool IsSent
        {
            get { return _isSent; }
            set { SetProperty(ref _isSent, value); }
        }

        IShoppingCartService shoppingCartService;

        IOrderService orderService;

        IStoreService storeService;
        public ICommand SendOrderCommand { get; set; }
        public AsyncCommand OkSentCommand { get; set; }
        public OrderCompletionViewModel()
        {
            shoppingCartService = DependencyService.Get<IShoppingCartService>();
            orderService = DependencyService.Get<IOrderService>();
            storeService = DependencyService.Get<IStoreService>();
            SendOrderCommand = new Command(SendOrder);
            OkSentCommand = new AsyncCommand(async () => { await Shell.Current.Navigation.PopToRootAsync(); });


            SetCurrentOrder();
        }
        private async void SetCurrentOrder()
        {
            Order = new OrderModel();
            Order.UserLogin = "admin";
            Order.StoreId = await shoppingCartService.GetStoreId();
            Order.StoreName = await storeService.GetStoreName(Order.StoreId);
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
            if (Seending)
                return;

            Seending = true;
            await IncludeFinalOrderData();
            MessagingCenter.Send(Order, "AddOrderToHistory");
            await orderService.AddUserOrder(Order);
            await shoppingCartService.ClearCart();
            await Task.Delay(1800);
            IsSent = true;
        }

        private async Task IncludeFinalOrderData()
        {
            var orderCount = await IncrementOrderCount();
            Order.OrderId = Order.StoreId.ToString("D4") + orderCount.ToString("D4");
            Order.OrderDateTime = DateTime.Now;
            Order.ShoppingCart = await shoppingCartService.GetCartList();
        }

        private async Task<int> IncrementOrderCount()
        {
            var orderCount = await orderService.GetOrderCount();
            orderCount++;
            return orderCount;
        }
    }
}
