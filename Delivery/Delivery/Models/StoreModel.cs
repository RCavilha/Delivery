using SQLite;
using System.Collections.Generic;

namespace Delivery.Models
{
    public class StoreModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int AddressNumber { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public List<StoreItemModel> StoreItems { get; set; }
    }
}
