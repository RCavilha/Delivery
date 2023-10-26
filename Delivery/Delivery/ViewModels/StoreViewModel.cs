using Delivery.Libraries.Helpers.MVVM;
using Delivery.Models;
using Delivery.Services;
using MvvmHelpers.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Delivery.ViewModels
{
    public class StoreViewModel : BaseViewModel
    {
        private List<StoreModel> _storeList;
        private IShoppingCartService _shoppingCartService;
        private IStoreService _storeService;

        public StoreViewModel()
        {
            StoreItemsCommand = new AsyncCommand<StoreModel>(StoreItemsGoTo);
            _shoppingCartService = DependencyService.Get<IShoppingCartService>();
            _storeService = DependencyService.Get<IStoreService>();
            GetListStore();
        }

        public AsyncCommand<StoreModel> StoreItemsCommand { get; set; }
        
        public List<StoreModel> StoreList
        {
            get { return _storeList; }
            set { SetProperty(ref _storeList, value); }
        }

        public bool ShowCartView
        {
            get
            {
                var quantity = _shoppingCartService.GetCount().Result;
                return (quantity > 0);
            }
        }

        public async void GetListStore()
        {
            StoreList = await _storeService.GetStoreList();
        }

        public async Task StoreItemsGoTo(StoreModel store)
        {
            await Shell.Current.GoToAsync($"store/items?StoreSerialized={store.Id}");
        }
    }
}
