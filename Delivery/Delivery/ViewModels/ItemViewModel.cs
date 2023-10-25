using Delivery.Libraries.Helpers.MVVM;
using Delivery.Models;
using Delivery.Services;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace Delivery.ViewModels
{
    [QueryProperty("StoreCode", "StoreCode")]
    [QueryProperty("selectedItemSerialized", "selectedItemSerialized")]
    public class ItemViewModel : BaseViewModel
    {
        IShoppingCartService _shoppingCartService;
        private int _itemsQuantity = 1;
        private int _storeCode = 0;
        private bool _showStoreChanged;

        public ItemViewModel()
        {
            AddItemClickCommand = new AsyncCommand(AddItemClick);
            ClearCartQuestionCommand = new AsyncCommand(ClearCartQuestion);
            LeaveCartQuestionCommand = new AsyncCommand(LeaveCartQuestion);
            IncItemsQuantityCommand = new Command(IncItemsQuantity);
            DecItemsQuantityCommand = new Command(DecItemsQuantity);
            _shoppingCartService = DependencyService.Get<IShoppingCartService>();
        }

        public StoreItemModel SelectedItem { get; set; }
        public AsyncCommand AddItemClickCommand { get; set; }
        public AsyncCommand ClearCartQuestionCommand { get; set; }
        public AsyncCommand LeaveCartQuestionCommand { get; set; }
        public ICommand IncItemsQuantityCommand { get; set; }
        public ICommand DecItemsQuantityCommand { get; set; }

        public int ItemsQuantity
        {
            get { return _itemsQuantity; }
            set
            {
                if (value <= 0)
                    _itemsQuantity = 1;
                else
                    _itemsQuantity = value;

                OnPropertyChanged(nameof(ItemsQuantity));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

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

        public int StoreCode
        {
            get { return _storeCode; }
            set { _storeCode = value; }
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

        public bool ShowStoreChanged
        {
            get { return _showStoreChanged; }
            set { SetProperty(ref _showStoreChanged, value); }
        }

        public async Task AddItemClick()
        {
            var cartStoreId = await _shoppingCartService.GetStoreId();

            if ((cartStoreId > 0) && (cartStoreId != StoreCode))
            {
                ShowStoreChanged = true;
                return;
            }

            await AddToCartAndNavigateBack(true);
        }

        public async Task ClearCartQuestion()
        {
            await _shoppingCartService.ClearCart();
            await AddToCartAndNavigateBack(true);
        }

        public async Task LeaveCartQuestion()
        {
            await AddToCartAndNavigateBack(false);
        }

        public async Task AddToCartAndNavigateBack(bool addItem)
        {
            if (addItem)
                await _shoppingCartService.AddItemToCart(_storeCode, SelectedItem.Id, SelectedItem.Image, SelectedItem.Name, SelectedItem.Price, ItemsQuantity);

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
    }
}
