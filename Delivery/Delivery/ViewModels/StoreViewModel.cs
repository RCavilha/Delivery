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
        private IShoppingCartService shoppingCartService;
        private IStoreService storeService;

        private List<StoreModel> _storeList;
        public List<StoreModel> StoreList
        {
            get
            {
                return _storeList;
            }
            set
            {
                SetProperty(ref _storeList, value);
            }
        }

        public bool ShowCartView
        {
            get
            {
                var quantity = shoppingCartService.GetCount().Result;
                return (quantity > 0);
            }
        }

        public AsyncCommand<StoreModel> StoreItemsCommand { get; set; }

        public StoreViewModel()
        {
            StoreItemsCommand = new AsyncCommand<StoreModel>(StoreItemsGoTo);
            shoppingCartService = DependencyService.Get<IShoppingCartService>();
            storeService = DependencyService.Get<IStoreService>();
            GetListStore();
        }

        public async void GetListStore()
        {
            StoreList = await storeService.GetStoreList();
        }
        public async Task StoreItemsGoTo(StoreModel store)
        {
            await Shell.Current.GoToAsync($"store/items?storeSerialized={store.Id}");
        }
    }
}
