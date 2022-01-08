using System.ComponentModel.DataAnnotations.Schema;

namespace ProductShop.Models
{
    public enum BuyerType
    {
        None,
        Regular,
        Golden,
        Wholesale
    }

    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public BuyerType BuyerType { get; set; }

        public int? CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }
    }
}
