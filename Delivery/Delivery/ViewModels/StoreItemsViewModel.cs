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
            get
            {
                return _storeModel;
            }
            set
            {
                _storeModel = value;

                //var items = new StoreItemsService();
                //Store.StoreItems = items.GetListStoreItems();
                OnPropertyChanged(nameof(Store));
                PageIsLoaded = true;
            }
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

        public StoreItemsViewModel()
        {
            PageIsLoaded = false;
            SelectedItemCommand = new Command<StoreItemModel>(SelectedItemGoTo);
            CartCommand = new Command(OpenCart);
        }

        public async void GetStoreDataBase()
        {
            await Task.Delay(150);
            var service = new StoreService();
            Store = await service.GetStore(idStore);

            //await service.SalvaEmpresa(Store);
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
    }
}
