using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms;
using SQLite;
using Newtonsoft.Json;

namespace Delivery.Models
{
    public class OrderModel
    {     
        public string OrderId { get; set; }
        public string UserLogin { get; set; }
        public int StoreId { get; set; }
        public string DeliveryType { get; set; }
        public string PaymentType { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalPrice { get; set; }
        public double DiscountValue { get; set; }
        public double TotalAmountToPay { get; set; }
        public List<ShoppingCartModel> ShoppingCart { get; set; }
        [JsonIgnore]
        public string StoreName { get; set; }
    }
}
