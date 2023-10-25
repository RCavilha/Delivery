using SQLite;
using System.ComponentModel;

namespace Delivery.Models
{
    public class ShoppingCartModel
    {
        [PrimaryKey]
        public int IdItem { get; set; }        
        public int IdStore { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
    }

}
