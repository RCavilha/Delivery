using Delivery.Libraries.Helpers.MVVM;
using Delivery.Models;
using Delivery.Services;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Delivery.ViewModels
{
    public class StoreViewModel : BaseViewModel
    {
        private List<StoreModel> _listOfStores;
        public List<StoreModel> ListOfStores
        {
            get
            {

                return _listOfStores;
            }
            set
            {
                SetProperty(ref _listOfStores, value);
            }
        }

        public AsyncCommand<StoreModel> StoreItemsCommand { get; set; }

        public StoreViewModel()
        {
            StoreItemsCommand = new AsyncCommand<StoreModel>(StoreItemsGoTo);
            GetListStore();
        }

        public async void GetListStore()
        {
            var service = new StoreService();
            ListOfStores = await service.GetStoreList();

            var items = new StoreItemsService();

            foreach (var store in ListOfStores)
            {
                store.StoreItems = items.GetListStoreItems();
            }
        }
        public async Task StoreItemsGoTo(StoreModel store)
        {
            await Shell.Current.GoToAsync($"store/items?storeSerialized={store.Id}");
        }
    }
}
