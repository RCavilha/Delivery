using Delivery.Views;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Delivery.Behaviors
{
    public class HideBagButtonOnScrollBehavior : Behavior<CollectionView>
    {
        private Button _bagButton;
        private ContentView _cartButtonsView;

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
            double itemHeight = 120;
            var collectionView = (CollectionView)sender;
            var visibleItemCount = (int)(collectionView.Height / itemHeight);
            var totalItemsCount = collectionView.ItemsSource?.Cast<object>().Count() ?? 0;
            var lastVisibleItemIndex = e.FirstVisibleItemIndex + visibleItemCount;

            if (_bagButton == null)
                _bagButton = collectionView.FindByName<Button>("bagButton");

            if (_cartButtonsView == null)
               _cartButtonsView = collectionView.FindByName<ContentView>("CartButtonsView");

            if (_bagButton != null)
                _bagButton.IsVisible = _cartButtonsView.IsVisible && !(lastVisibleItemIndex >= totalItemsCount - 1);
        }

    }

}
