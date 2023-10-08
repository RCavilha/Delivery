using Delivery.Libraries.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

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
        public string Phone { get; set; }
        public List<StoreItemModel> StoreItems { get; set; }
    }
}
