using Delivery.Libraries.Helpers.MVVM;
using Delivery.Models;
using Delivery.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Delivery.ViewModels
{
    public class OrderHistoryViewModel : BaseViewModel
    {
        private ObservableCollection<OrderModel> _orderList;
        public ObservableCollection<OrderModel> OrderList
        {
            get
            {
                return _orderList;
            }
            set
            {
                SetProperty(ref _orderList, value);
            }
        }

        IOrderService orderService;
        public OrderHistoryViewModel()
        {
            orderService = DependencyService.Get<IOrderService>();
            
            MessagingCenter.Subscribe<OrderModel>(this, "AddOrderToHistory", (add) =>
            {
                if(OrderList  != null) 
                {
                    OrderList.Insert(0, add);
                }                
            });

            GetOrderList();
        }
        ~OrderHistoryViewModel()
        {
            MessagingCenter.Unsubscribe<OrderModel>(this, "AddOrderToHistory");
        }
        public async void GetOrderList()
        {
            var service = new StoreService();
            var storeList = await service.GetStoreList();
            var list = await orderService.GetOrderList("admin");

            foreach (var item in list)
            {
                // Encontre a loja correspondente na storeList usando LINQ
                var matchedStore = storeList.FirstOrDefault(store => store.Id == item.StoreId);

                // Se a loja correspondente for encontrada, atribua o nome da loja ao StoreName do item
                if (matchedStore != null)
                {
                    item.StoreName = matchedStore.Name;
                }
            }

            // Ordene a lista de pedidos pela data do pedido da mais recente para a mais antiga
            var orderedList = list.OrderByDescending(order => order.OrderDateTime).ToList();

            OrderList = new ObservableCollection<OrderModel>(orderedList);
        }
    }
}
