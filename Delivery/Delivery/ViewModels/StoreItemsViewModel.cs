using Delivery.Libraries.Helpers.MVVM;
using Delivery.Models;
using Newtonsoft.Json;
using System;
using System.Windows.Input;
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

        public ICommand SelectedItemCommand { get; set; }
        public ICommand CartCommand { get; set; }

        public StoreItemsViewModel()
        {
            SelectedItemCommand = new Command<StoreItemModel>(SelectedItemGoTo);
            CartCommand = new Command(OpenCart);
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
