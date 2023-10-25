using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Delivery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : Shell
    {
        public Menu()
        {
            InitializeComponent();

            Routing.RegisterRoute("store/items", typeof(Views.StoreItemsView));
            Routing.RegisterRoute("store/items/selecteditem", typeof(Views.ItemView));
            Routing.RegisterRoute("store/cart", typeof(Views.ShoppingCartView));
            Routing.RegisterRoute("cart/ordercompletion", typeof(Views.OrderCompletionView));
        }
    }
}