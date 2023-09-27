using Delivery.Models;
using Delivery.Repositories;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Xamarin.Essentials.Permissions;
using static Xamarin.Forms.Device;

namespace Delivery.Services
{
    public class StoreService
    {
        private static string _tableName = "Stores";
        public static string FireBasePassword = "H96LnlDzvh0rZ9sJ3bnUGN0Pj9V8qkUyD9eZd9qq";
        FirebaseClient _dbStore = new FirebaseClient("https://delivery-69a12-default-rtdb.firebaseio.com/", new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(FireBasePassword) });

        public async Task<bool> SalvaEmpresa(StoreModel stores)
        {
            try
            {
                await _dbStore.Child(_tableName).
                    PostAsync(new StoreRepository()
                    {
                        Id = stores.Id,
                        Logo = stores.Logo,
                        Name = stores.Name,
                        Description = stores.Description,
                        Address = stores.Address,
                        Phone = stores.Phone,
                    });
                return true;
            }
            catch
            {
                return false;
            }
        }



        public async Task<List<StoreModel>> GetListOfStores()
        {

            var stores = new List<StoreModel>();

            var response = await _dbStore
                .Child(_tableName)
                .OnceAsync<StoreRepository>();

            foreach (var item in response)
            {
                var storeRepository = item.Object;

                stores.Add(new StoreModel()
                {
                    Id = storeRepository.Id,
                    Logo = storeRepository.Logo,
                    Name = storeRepository.Name,
                    Description = storeRepository.Description,
                    Address = storeRepository.Address,
                    Phone = storeRepository.Phone,
                });
            }

            return stores;



            /* var oi = new StoreModel()
             {
                 Id = 1,
                 Logo = "https://botw-pd.s3.amazonaws.com/styles/logo-thumbnail/s3/102014/logo_outback.png?itok=dKkkYi4q",
                 Name = "OUTBACK",
                 Description = "Outback Steakhouse",
                 Address = "3 Andar - Loja 01/02/03/04 - Entrada Sul",
                 Phone = "(61) 3550-1874",
             };
             var oi2 = new StoreModel()
             {
                 Id = 2,
                 Logo = "https://th.bing.com/th/id/OIP.rkuaDq-syjT-EZdhuQ6GfgHaGU?pid=Api&rs=1",
                 Name = "GIRAFFAS",
                 Description = "O Giraffas é uma rede de restaurantes de culinária brasileira que serve seus pratos de forma saborosa e criativa.",
                 Address = "3 Andar - Loja 05/06/07 - Entrada Sul",
                 Phone = "(61) 3550-1874",
             };



             SalvaEmpresa(oi);
             SalvaEmpresa(oi2);


             List<StoreModel> listOfStore = new List<StoreModel>();
             listOfStore.Add(oi);
             listOfStore.Add(oi2);

             return listOfStore;*/
        }
    }
}
