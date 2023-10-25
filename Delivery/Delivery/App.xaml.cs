using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
