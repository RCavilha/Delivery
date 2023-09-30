using Delivery.Libraries.Helpers.MVVM;
using Delivery.Models;
using Delivery.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Delivery.ViewModels
{
    [QueryProperty("storeSerialized", "storeSerialized")]
    public class StoreItemsViewModel : BaseViewModel
    {
        public StoreModel Store { get; set; }
        public string storeSerialized 
        {
            set
            {
                Store = JsonConvert.DeserializeObject<StoreModel>(Uri.UnescapeDataString(value));
                OnPropertyChanged(nameof(Store));
            }
        }

        private List<StoreItemModel> _listStoreItems;
        public List<StoreItemModel> ListStoreItems
        {
            get
            {

                return _listStoreItems;
            }
            set
            {
                SetProperty(ref _listStoreItems, value);
            }
        }

        public StoreItemsViewModel()
        {
            GetListItems();
        }

        public async void GetListItems()
        {
            var service = new StoreItemsService();
            ListStoreItems = await service.GetListStoreItems();
        }
    }
}
