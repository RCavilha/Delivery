using Delivery.Libraries.Helpers.MVVM;
using Delivery.Models;
using Delivery.Services;
using System.Collections.Generic;
using System.Windows.Input;
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

        public ICommand StoreItemsCommand { get; set; }

        public StoreViewModel()
        {
            StoreItemsCommand = new Command<StoreModel>(StoreItemsGoTo);
            GetListStore();
        }

        public async void GetListStore()
        {
            var service = new StoreService();
            ListOfStores = await service.GetStoreList();
        }
        public void StoreItemsGoTo(StoreModel store)
        {
            Shell.Current.GoToAsync("store/items");
        }
    }
}
