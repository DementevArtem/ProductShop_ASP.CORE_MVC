using System.Collections.Generic;

namespace ProductShop.Models
{
    public enum Discount
    {
        O, R, V
    }
    public class Customer
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public Discount? Discount { get; set; }

        public User User { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
