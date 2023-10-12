﻿using Delivery.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                await _dbStore.Child(_tableName).PostAsync(stores);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<StoreModel>> GetStoreList()
        {
            var firebaseObjects = await _dbStore
                .Child(_tableName)
                .OnceAsync<StoreModel>();
            var storeModels = firebaseObjects
                .Select(firebaseObject => firebaseObject.Object)
                .ToList();
            return storeModels;
        }

        public async Task<StoreModel> GetStore(int idStore)
        {
            var storeSelect = (await _dbStore
                    .Child(_tableName)
                    .OnceAsync<StoreModel>())
                    .Where(store => store.Object.Id == idStore)
                    .FirstOrDefault();

            return storeSelect.Object;
        }
    }
}
