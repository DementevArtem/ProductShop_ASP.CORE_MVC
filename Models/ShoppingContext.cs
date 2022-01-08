using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProductShop.Models
{
    public class ShoppingContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SuperMarket> SuperMarkets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public ShoppingContext(DbContextOptions<ShoppingContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminEmail = "admin@gmail.com";
            string adminPassword = "1234";

            string regBuyerEmail = "reg.buyer@gmail.com";
            string vipBuyerEmail = "vip.buyer@gmail.com";
            string buyerPassword = "1234";

            // adding roles
            Role adminRole = new Role { Id = 1, Name = "admin" };
            Role buyerRole = new Role { Id = 2, Name = "buyer" };

            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id, BuyerType = BuyerType.None };
            User regBuyerUser = new User { Id = 2, Email = regBuyerEmail, Password = buyerPassword, RoleId = buyerRole.Id, BuyerType = BuyerType.Regular };
            User vipBuyerUser = new User { Id = 3, Email = vipBuyerEmail, Password = buyerPassword, RoleId = buyerRole.Id, BuyerType = BuyerType.Golden };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, buyerRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser, regBuyerUser, vipBuyerUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}
