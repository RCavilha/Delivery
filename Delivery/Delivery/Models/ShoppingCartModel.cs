using SQLite;
using System.ComponentModel;

namespace Delivery.Models
{
    public class ShoppingCartModel : BaseModel
    {
        [PrimaryKey]
        public int IdItem { get; set; }        
        public int IdStore { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }

        private double _totalPrice;
        public double TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                if (_totalPrice != value)
                {
                    _totalPrice = value;
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }
    }

}
