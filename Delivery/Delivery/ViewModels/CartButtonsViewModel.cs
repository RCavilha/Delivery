﻿using Delivery.Services;
using Delivery.Views;
using MvvmHelpers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Delivery.ViewModels
{
    public class CartButtonsViewModel : BaseViewModel
    {
        private IShoppingCartService shoppingCartService;
        public ICommand GotoCartCommand { get; set; }

        private bool _showCartButtonsView = false;
        public bool ShowCartButtonsView
        {
            get { return _showCartButtonsView; }
            private set
            {
                SetProperty(ref _showCartButtonsView, value);
            }
        }
            
        private int _quantity = 0;
        public int Quantity
        {
            get { return _quantity; }
            private set 
            {
                ShowCartButtonsView = value > 0;
                SetProperty(ref _quantity, value); 
            }
        }

        private double _total = 0;
        public double Total
        {
            get { return _total; }
            private set { SetProperty(ref _total, value); }
        }

        public CartButtonsViewModel()
        {
            shoppingCartService = DependencyService.Get<IShoppingCartService>();
            GotoCartCommand = new Command(OpenCart);

            MessagingCenter.Subscribe<ShoppingCartService>(this, "CartUpdate", async (upd) =>
            {
                await UpdateTotalAsync();
            });

            UpdateTotalAsync(); // Chame o método para atualizar o total quando o ViewModel for criado
        }

        ~CartButtonsViewModel()
        {
            MessagingCenter.Unsubscribe<ShoppingCartService>(this, "CartUpdate");
        }
        
        private async Task UpdateTotalAsync()
        {
            Total = await shoppingCartService.GetTotalPrice();
            Quantity = await shoppingCartService.GetCount();
        }

        public void OpenCart()
        {
            Shell.Current.GoToAsync($"store/cart");
        }
    }
}
