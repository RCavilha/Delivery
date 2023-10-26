using Delivery.Libraries.Helpers.MVVM;
using Delivery.Models;
using Delivery.Services;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Delivery.ViewModels
{
    public class OrderHistoryViewModel : BaseViewModel
    {
        private bool _pageIsLoaded = false;
        private IOrderService _orderService;
        private IStoreService _storeService;
        private ObservableCollection<OrderModel> _orderList;

        public OrderHistoryViewModel()
        {
            _orderService = DependencyService.Get<IOrderService>();
            _storeService = DependencyService.Get<StoreService>();
            MessagingCenter.Subscribe<OrderModel>(this, "AddOrderToHistory", (add) =>
            {
                if (OrderList != null)
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

        public bool PageIsLoaded
        {
            get { return _pageIsLoaded; }
            set { SetProperty(ref _pageIsLoaded, value); }
        }

        public ObservableCollection<OrderModel> OrderList
        {
            get { return _orderList; }
            set { SetProperty(ref _orderList, value); }
        }

        public async void GetOrderList()
        {
            var storeList = await _storeService.GetStoreList();
            var list = await _orderService.GetOrderList("admin");
            //Faz o LINQ na list em vez de filtrar a tabela um a um para melhor desempenho 
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
            PageIsLoaded = true;
        }
    }
}
