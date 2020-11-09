using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using WebVL.Models;

namespace WebVL.Context
{
    public class ProductContext: IdentityDbContext<ApplicationUser>
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Statt> Statts { get; set; }
        public DbSet<AdminAccount> AdminAccounts { get; set; }
        public DbSet<OrdersDetails> ordersDetails { get; set; }
        public ProductContext() : base("ProductContext", throwIfV1Schema: false)
        {
        }

        public static ProductContext Create()
        {
            return new ProductContext();
        }


        //public System.Data.Entity.DbSet<WebVL.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}