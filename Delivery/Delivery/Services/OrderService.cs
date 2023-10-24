using Delivery.Models;
using Delivery.Services;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(OrderService))]
public class OrderService : IOrderService
{
    private static string _tableName = "Orders";
    public static string FireBasePassword = "H96LnlDzvh0rZ9sJ3bnUGN0Pj9V8qkUyD9eZd9qq";
    FirebaseClient _dbStore;

    void Init()
    {
        if (_dbStore != null)
            return;

        _dbStore = new FirebaseClient("https://delivery-69a12-default-rtdb.firebaseio.com/", new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(FireBasePassword) });
    }

    public async Task<bool> AddUserOrder(OrderModel order)
    {
        Init();
        try
        {
            await _dbStore.Child(_tableName).PostAsync(order);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<OrderModel>> GetOrderList(string user)
    {
        Init();
        var firebaseObjects = await _dbStore
            .Child(_tableName)
            .OnceAsync<OrderModel>();
        var orderList = firebaseObjects.Where(store => store.Object.UserLogin == user)
            .Select(firebaseObject => firebaseObject.Object)
            .ToList();

        return orderList;
    }

    public async Task<int> GetOrderCount()
    {
        Init();
        var firebaseObjects = await _dbStore.Child(_tableName).OnceAsync<OrderModel>();
        return firebaseObjects.Count();
    }
}
