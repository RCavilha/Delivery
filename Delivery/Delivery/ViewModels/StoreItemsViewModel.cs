using Delivery.Libraries.Helpers.MVVM;
using Delivery.Models;
using Delivery.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Delivery.ViewModels
{
    [QueryProperty("storeSerialized", "storeSerialized")]
    public class StoreItemsViewModel : BaseViewModel
    {
        public int idStore { get; set; }

        private bool _pageIsLoaded = false;
        public bool PageIsLoaded
        {
            get
            {
                return _pageIsLoaded;
            }
            set
            {
                _pageIsLoaded = value;
                OnPropertyChanged(nameof(PageIsLoaded));
            }
        }

        private StoreModel _storeModel;
        public StoreModel Store
        {
            get { return _storeModel; }
            set
            {
                SetProperty(ref _storeModel, value);
                PageIsLoaded = true;
            }
        }

        private int _quantityCartItem;
        public int QuantityCartItem
        {
            get { return _quantityCartItem;}
            set { SetProperty(ref _quantityCartItem, value); }
        }

        public int storeSerialized
        {
            set
            {
                if (idStore != value)
                {
                    idStore = value;
                    GetStoreDataBase();
                }
            }
        }
        public ICommand SelectedItemCommand { get; set; }
        public ICommand CartCommand { get; set; }

        IShoppingCartService shoppingCartService;

        IStoreService storeService;
        public StoreItemsViewModel()
        {
            PageIsLoaded = false;
            SelectedItemCommand = new Command<StoreItemModel>(SelectedItemGoTo);
            CartCommand = new Command(OpenCart);
            shoppingCartService = DependencyService.Get<IShoppingCartService>();
            storeService = DependencyService.Get<IStoreService>();
        }

        public async void GetStoreDataBase()
        {
            await Task.Delay(150);
            Store = await storeService.GetStore(idStore);
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

        public async void UpdateQuantityCartItem()
        {
            QuantityCartItem = await shoppingCartService.GetTotalQuantityItems();
        }
    }
}
