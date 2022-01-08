using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
