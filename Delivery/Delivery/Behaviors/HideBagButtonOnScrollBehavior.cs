using Delivery.Views;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Delivery.Behaviors
{
    public class HideBagButtonOnScrollBehavior : Behavior<CollectionView>
    {
        private Grid _bagGrid;
        private ContentView _cartButtonsView;
        private RowDefinition _rowCartButtonsView;
        private int _totalItemsCount = 0;

        protected override void OnAttachedTo(CollectionView collectionView)
        {
            base.OnAttachedTo(collectionView);
            collectionView.Scrolled += OnCollectionViewScrolled;
        }

        protected override void OnDetachingFrom(CollectionView collectionView)
        {
            base.OnDetachingFrom(collectionView);
            collectionView.Scrolled -= OnCollectionViewScrolled;
        }

        private void OnCollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            var collectionView = (CollectionView)sender;

            if (_totalItemsCount == 0)
                _totalItemsCount = collectionView.ItemsSource?.Cast<object>().Count() ?? 0;

            if (_bagGrid == null)
                _bagGrid = collectionView.FindByName<Grid>("bagGrid");

            if (_cartButtonsView == null)
                _cartButtonsView = collectionView.FindByName<ContentView>("CartButtonsView");

            if (_rowCartButtonsView == null)
                _rowCartButtonsView = collectionView.FindByName<RowDefinition>("RowCartButton");

            if ((_bagGrid != null) && (_cartButtonsView != null))
            {
                _bagGrid.IsVisible = _cartButtonsView.IsVisible && !(e.LastVisibleItemIndex >= _totalItemsCount);

                if ((_cartButtonsView.IsVisible) && (!_bagGrid.IsVisible))
                {
                    _rowCartButtonsView.Height = 60;
                }
                else
                {
                    _rowCartButtonsView.Height = 0;
                }
            }
                
        }

    }

}
