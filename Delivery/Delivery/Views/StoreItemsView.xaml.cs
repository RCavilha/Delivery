using Delivery.ViewModels;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Delivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreItemsView : ContentPage
    {
        private StoreItemsViewModel _viewModel;

        private int _totalItemsCount = 0;
        private int _lastVisibleItemIndex = 0;

        public StoreItemsView()
        {
            InitializeComponent();
            _viewModel = new StoreItemsViewModel();
            BindingContext = _viewModel;

            
            MessagingCenter.Subscribe<ItemViewModel>(this, "CartButtonUpdate", async (upd) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {

                });
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.UpdateQuantityCartItem();
            ControlCartButton();
        }

        ~StoreItemsView()
        {
            MessagingCenter.Unsubscribe<ItemViewModel>(this, "CartButtonUpdate");
        }

        private void CollectionStoreItems_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if (_totalItemsCount == 0)
            {
                var collectionView = (CollectionView)sender;
                if (_totalItemsCount == 0)
                    _totalItemsCount = collectionView.ItemsSource?.Cast<object>().Count() ?? 0;
            }
            _lastVisibleItemIndex = e.LastVisibleItemIndex;
            ControlCartButton();
        }
        private void ControlCartButton()
        {
            if (_viewModel.QuantityCartItem > 0)
            {
                CartButtonsView.IsVisible = (_lastVisibleItemIndex >= _totalItemsCount);
                bagGrid.IsVisible = !CartButtonsView.IsVisible;
            }
            else
            {
                CartButtonsView.IsVisible = false;
                bagGrid.IsVisible = false;
            }

            if (CartButtonsView.IsVisible)
            {
                RowCartButton.Height = 60;
            }
            else
            {
                RowCartButton.Height = 0;
            }
        }
    }
}