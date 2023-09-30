using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Delivery.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreItemsView : ContentPage
    {
        public StoreItemsView()
        {
            InitializeComponent();
        }

        private void CollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {

        }
    }
}