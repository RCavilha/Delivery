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
        private int _itemsQuantity = 1;
        public int ItemsQuantity
        {
            get
            {
                return _itemsQuantity;
            }
            set
            {
                if (value <= 0)
                {
                    _itemsQuantity = 1;
                }
                else
                {
                    _itemsQuantity = value;
                }
                OnPropertyChanged(nameof(ItemsQuantity));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public StoreItemModel SelectedItem { get; set; }
        public string selectedItemSerialized
        {
            set
            {
                SelectedItem = JsonConvert.DeserializeObject<StoreItemModel>(Uri.UnescapeDataString(value));
                OnPropertyChanged(nameof(SelectedItem));
                OnPropertyChanged(nameof(TotalPrice));
                OnPropertyChanged(nameof(ItemsQuantity));
            }
        }

        public double TotalPrice
        {
            get
            {
                if(SelectedItem == null)
                    return 0;

                return  SelectedItem.Price * _itemsQuantity;
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
            ItemsQuantity--;
        }

        public ItemViewModel()
        {
            IncItemsQuantityCommand = new Command(IncItemsQuantity);
            DecItemsQuantityCommand = new Command(DecItemsQuantity);
            ItemsQuantity = 1;
        }
    }
}
