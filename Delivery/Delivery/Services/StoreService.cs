﻿using Delivery.Models;
using Delivery.Services;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(StoreService))]
namespace Delivery.Services
{
    public class StoreService : IStoreService
    {
        private static string _tableName = "Stores";
        public static string FireBasePassword = "H96LnlDzvh0rZ9sJ3bnUGN0Pj9V8qkUyD9eZd9qq";
        FirebaseClient _dbStore;

        void Init()
        {
            if (_dbStore != null)
                return;
            _dbStore = new FirebaseClient("https://delivery-69a12-default-rtdb.firebaseio.com/", new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(FireBasePassword) });
        }

        public async Task<bool> SaveStore(StoreModel stores)
        {
            Init();
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
            Init();
            var firebaseObjects = await _dbStore
                .Child(_tableName)
                .OnceAsync<StoreModel>();

            if (firebaseObjects != null)
            {
                var storeModels = firebaseObjects
                    .Select(firebaseObject => firebaseObject.Object)
                    .ToList();

                if (storeModels != null)
                    return storeModels;
            }

            return new List<StoreModel>();
        }
        public async Task<StoreModel> GetStore(int idStore)
        {
            Init();
            var storeSelect = (await _dbStore
                    .Child(_tableName)
                    .OnceAsync<StoreModel>())
                    .Where(store => store.Object.Id == idStore)
                    .FirstOrDefault();

            if (storeSelect != null)
                return storeSelect.Object;

            return new StoreModel();
        }

        public async Task<string> GetStoreName(int idStore)
        {
            Init();
            var storeSelect = (await _dbStore
                    .Child(_tableName)
                    .OnceAsync<StoreModel>())
                    .Where(store => store.Object.Id == idStore)
                    .FirstOrDefault();

            if ((storeSelect == null) || (storeSelect.Object == null))
                return string.Empty;

            return storeSelect.Object.Name;
        }
    }
}
