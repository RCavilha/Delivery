using Delivery.Libraries.Helpers.MVVM;
using Delivery.Models;
using Delivery.Services;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Delivery.ViewModels
{
    [QueryProperty("StoreSerialized", "StoreSerialized")]
    public class StoreItemsViewModel : BaseViewModel
    {
        private bool _pageIsLoaded = false;
        private int _quantityCartItem;
        private StoreModel _storeModel;
        private IShoppingCartService _shoppingCartService;
        private IStoreService _storeService;

        public StoreItemsViewModel()
        {
            PageIsLoaded = false;
            SelectedItemCommand = new Command<StoreItemModel>(SelectedItemGoTo);
            CartCommand = new Command(OpenCart);
            _shoppingCartService = DependencyService.Get<IShoppingCartService>();
            _storeService = DependencyService.Get<IStoreService>();
        }

        public int IdStore { get; set; }
        public ICommand SelectedItemCommand { get; set; }
        public ICommand CartCommand { get; set; }

        public bool PageIsLoaded
        {
            get { return _pageIsLoaded; }
            set { SetProperty(ref _pageIsLoaded, value); }
        }

        public int QuantityCartItem
        {
            get { return _quantityCartItem; }
            set { SetProperty(ref _quantityCartItem, value); }
        }

        public StoreModel Store
        {
            get { return _storeModel; }
            set
            {
                SetProperty(ref _storeModel, value);
                PageIsLoaded = true;
            }
        }

        public int StoreSerialized
        {
            set
            {
                if (IdStore != value)
                {
                    IdStore = value;
                    GetStoreDataBase();
                }
            }
        }

        public async void GetStoreDataBase()
        {
            await Task.Delay(150);
            Store = await _storeService.GetStore(IdStore);
        }

        public void SelectedItemGoTo(StoreItemModel selectedItem)
        {
            string selectedItemSerialized = JsonConvert.SerializeObject(selectedItem);
            Shell.Current.GoToAsync($"store/items/selecteditem?StoreCode={Store.Id}&selectedItemSerialized={Uri.EscapeDataString(selectedItemSerialized)}");
        }

        public void OpenCart()
        {
            Shell.Current.GoToAsync($"store/cart");
        }

        public async Task UpdateQuantityCartItem()
        {
            QuantityCartItem = await _shoppingCartService.GetTotalQuantityItems();
        }
    }
}
