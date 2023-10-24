using Delivery.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Delivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartButtonsView : ContentView
    {
        private CartButtonsViewModel _viewModel;
        public CartButtonsView()
        {
            InitializeComponent();
            _viewModel = new CartButtonsViewModel();
            BindingContext = _viewModel;
        }
        public void UpdateTotal()
        {
            _viewModel.UpdateTotal();
        }
    }
}