using Delivery.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Services
{
    public interface IOrderService
    {
        Task<bool> AddUserOrder(OrderModel order);
        Task<List<OrderModel>> GetOrderList(string user);
        Task<int> GetOrderCount();
    }
}
