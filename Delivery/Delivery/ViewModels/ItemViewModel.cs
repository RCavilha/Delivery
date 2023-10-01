using Delivery.Libraries.Helpers.MVVM;
using Delivery.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Delivery.ViewModels
{
    [QueryProperty("selectedItemSerialized", "selectedItemSerialized")]
    public class ItemViewModel : BaseViewModel
    {
        private int itemsQuantity = 1;
        public int ItemsQuantity
        {
            get
            {
                return itemsQuantity;
            }
            set
            {
                if(value <= 0)
                {
                    itemsQuantity = 1;
                }
                else
                {
                    itemsQuantity = value;
                }              
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public StoreItemModel SelectedItem { get; set; }
        public string selectedItemSerialized
        {
            set
            {
                SelectedItem = JsonConvert.DeserializeObject<StoreItemModel>(Uri.UnescapeDataString(value));
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public ICommand IncItemsQuantityCommand { get; set; }
        public ICommand DecItemsQuantityCommand { get; set; }

        public void IncItemsQuantity()
        {
            ItemsQuantity++;
        }

        public void DecItemsQuantity()
        {
            itemsQuantity--;
        }
    }
}
