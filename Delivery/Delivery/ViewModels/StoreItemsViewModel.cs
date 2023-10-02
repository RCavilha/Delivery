using Delivery.Libraries.Helpers.MVVM;
using Delivery.Models;
using Delivery.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public StoreItemsViewModel()
        {
            SelectedItemCommand = new Command<StoreItemModel>(SelectedItemGoTo);
        
        }

        public void SelectedItemGoTo(StoreItemModel selectedItem)
        {
            string selectedItemSerialized = JsonConvert.SerializeObject(selectedItem);
            Shell.Current.GoToAsync($"store/items/selecteditem?selectedItemSerialized={Uri.EscapeDataString(selectedItemSerialized)}");
        }
    }
}
