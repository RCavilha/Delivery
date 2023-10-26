using Delivery.Libraries.Helpers.MVVM;
using SQLite;

namespace Delivery.Models
{
    public class ShoppingCartModel : BaseModel
    {
        private int _quantity = 0;

        [PrimaryKey]
        public int IdItem { get; set; }
        public int IdStore { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }

        public int Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }
    }

}
