using Delivery.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Delivery.Services
{
    public class StoreItemsService
    {
        public async Task<List<StoreItemModel>> GetListStoreItems()
        {
            var listStoreItem = new List<StoreItemModel>()
           {
                new StoreItemModel()
                {
                    Id =1,
                    Image = "https://deliveryportodegalinhas.com.br/wp-content/uploads/2017/08/x-burguer.jpg",
                    Name ="Cheeseburguer",
                    Description = "Um hamburguer (100% carne bovina), queijo cheddar, cebola, picles, ketchup, mostarda e pão sem gergelim",
                    Price = 12.99,
                },

                new StoreItemModel()
                {
                    Id =2,
                    Image = "https://uploads.metropoles.com/wp-content/uploads/2019/04/11180534/big-four1.jpg",
                    Name ="Big Mac",
                    Description = "Dois hambúrgueres (100% carne bovina), alface americana, queijo cheddar, maionese Big Mac, cebola, picles e pão com gergelim",
                    Price = 26.99,
                }
           };

            return listStoreItem;
        }
    }
}
