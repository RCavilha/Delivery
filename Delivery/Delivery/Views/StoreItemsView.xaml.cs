using Delivery.ViewModels;
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
 

        protected override bool OnBackButtonPressed()
        {
            MessagingCenter.Send(this, "CartUpdateFromStoreItems");
            return base.OnBackButtonPressed();
        }
    }
}