using Delivery.Services;
using MvvmHelpers;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Delivery.ViewModels
{
    public class CartButtonsViewModel : BaseViewModel
    {
        private IShoppingCartService _shoppingCartService;
        private bool _showCartButtonsView = false;
        private int _quantity = 0;
        private double _total = 0;

        public CartButtonsViewModel()
        {
            _shoppingCartService = DependencyService.Get<IShoppingCartService>();
            GotoCartCommand = new Command(OpenCart);
            UpdateTotalAsync();
        }

        public ICommand GotoCartCommand { get; set; }
        
        public bool ShowCartButtonsView
        {
            get { return _showCartButtonsView; }
            private set { SetProperty(ref _showCartButtonsView, value); }
        }

        public int Quantity
        {
            get { return _quantity; }
            private set
            {
                ShowCartButtonsView = value > 0;
                SetProperty(ref _quantity, value);
            }
        }

        public double Total
        {
            get { return _total; }
            private set { SetProperty(ref _total, value); }
        }

        public async Task UpdateTotalAsync()
        {
            Total = await _shoppingCartService.GetTotalPrice();
            Quantity = await _shoppingCartService.GetCount();
        }

        public void OpenCart()
        {
            Shell.Current.GoToAsync($"store/cart");
        }
    }
}
