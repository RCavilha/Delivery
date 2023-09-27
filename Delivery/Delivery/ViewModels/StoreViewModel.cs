using Delivery.Models;
using Delivery.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Delivery.ViewModels
{
    public class StoreViewModel
    {
        public Task<List<StoreModel>> ListOfStores { get; set; }

        public StoreViewModel()
        {

            BuscandoLista();
        }


        public async void BuscandoLista()
        {
            var service = new StoreService();
            ListOfStores = service.GetListOfStores();
        }
    }
}
