using Delivery.Libraries.Helpers.MVVM;
using Delivery.Models;
using Delivery.Services;
using System.Collections.Generic;

namespace Delivery.ViewModels
{
    public class StoreItemsViewModel : BaseViewModel
    {
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
