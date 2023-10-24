﻿using Delivery.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Services
{
    internal interface IStoreService
    {
        Task<bool> SaveStore(StoreModel stores);
        Task<List<StoreModel>> GetStoreList();
        Task<StoreModel> GetStore(int idStore);
        Task<string> GetStoreName(int idStore);
    }
}
