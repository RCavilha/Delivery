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
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Shell.SetTabBarIsVisible(this, false);            
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Shell.SetTabBarIsVisible(this, true);
        }

        protected override bool OnBackButtonPressed()
        {
            //return true to prevent back, return false to just do something before going back. 
            MessagingCenter.Send(this, "CartUpdateFromStoreItems");
            return true;
        }
    }
}