using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductShop.Models;

namespace ProductShop
{
    public class SampleData
    {
        public static void Initialize(ShoppingContext context)
        {
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }
            context.Products.AddRange(
                    new Product
                    {
                        Name = "Butter",
                        Price = 30.0
                    },
                    new Product
                    {
                        Name = "Banana",
                        Price = 20.50
                    },
                    new Product
                    {
                        Name = "Cola",
                        Price = 9.30
                    }
                );
            context.SaveChanges();
            context.Customers.AddRange(
                new Customer
                {
                    FirstName = "Artem",
                    LastName = "Balaganov",
                    Address = "Odessa",
                    Discount = Discount.R,

                },
                new Customer
                {
                    FirstName = "Ostap",
                    LastName = "Bender",
                    Address = "Rio de Zhmerinka",
                    Discount = Discount.O,

                }
                );
            context.SaveChanges();
            context.SuperMarkets.AddRange(
                new SuperMarket
                {
                    Name = "Atlas",
                    Address = "Verdansk",

                },
                new SuperMarket
                {
                    Name = "Billa",
                    Address = "Odessa",

                },
                new SuperMarket
                {
                    Name = "Wellmart",
                    Address = "Lviv",

                }
                );
            context.SaveChanges();
            context.Orders.AddRange(
                    new Order
                    {
                        CustomerId = 1,
                        SuperMarketId = 2,
                        OrderDate = DateTime.Now,
                    },
                    new Order
                    {
                        CustomerId = 1,
                        SuperMarketId = 2,
                        OrderDate = DateTime.Now,
                    },
                    new Order
                    {
                        CustomerId = 2,
                        SuperMarketId = 1,
                        OrderDate = DateTime.Now,
                    }
                );
            context.SaveChanges();
            context.OrderDetails.AddRange(
                    new OrderDetail
                    {
                        OrderId = 1,
                        ProductId = 1,
                        Quantity = 2

                    },
                    new OrderDetail
                    {
                        OrderId = 2,
                        ProductId = 2,
                        Quantity = 1
                    },
                    new OrderDetail
                    {
                        OrderId = 3,
                        ProductId = 2,
                        Quantity = 2
                    }
                );
            context.SaveChanges();
            context.Users.AddRange(
                    new User
                    {
                        Email = "artem@gmail.com",
                        Password = "1111",
                        RoleId = 2,
                        CustomerId = 1
                    }
                );
            context.SaveChanges();
        }
    }
}
