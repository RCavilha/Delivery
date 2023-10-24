using Delivery.Libraries.Helpers.MVVM;
using Delivery.Models;
using Delivery.Services;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Command = MvvmHelpers.Commands.Command;

namespace Delivery.ViewModels
{
    [QueryProperty("StoreCode", "StoreCode")]
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
        private int _storeCode { get; set; }
        public int StoreCode
        {
            get { return _storeCode; }
            set
            {
                _storeCode = value;
            }
        }

        public double TotalPrice
        {
            get
            {
                if (SelectedItem == null)
                    return 0;

                return SelectedItem.Price * _itemsQuantity;
            }
        }

        public AsyncCommand AddItemCommand { get; set; }
        public ICommand IncItemsQuantityCommand { get; set; }
        public ICommand DecItemsQuantityCommand { get; set; }

        IShoppingCartService shoppingCartService;

        public async Task AddItem()
        {
            await shoppingCartService.AddItemToCart(_storeCode, SelectedItem.Id, SelectedItem.Image, SelectedItem.Name, SelectedItem.Price, ItemsQuantity);
            MessagingCenter.Send(this, "CartButtonUpdate");
            await Shell.Current.Navigation.PopAsync();
        }

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
            AddItemCommand = new AsyncCommand(AddItem);
            IncItemsQuantityCommand = new Command(IncItemsQuantity);
            DecItemsQuantityCommand = new Command(DecItemsQuantity);
            shoppingCartService = DependencyService.Get<IShoppingCartService>();
        }
    }
}
