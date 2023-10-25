using Xamarin.Forms;

namespace Delivery
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Menu();
        }
    }
}
