using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using WebVL.Context;

namespace WebVL.Models
{
    public class ShoppingCart
    {
        ProductContext db = new ProductContext();
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Img { get; set; }
        public int Number { get; set; }

        public double Total { get { return Price*Number; } }

        public ShoppingCart (string id)
        {
            Id = id;
            Product product = db.Products.Single(n => n.ProductId == id);
            Name = product.productName;
            Img = product.productImg;
            Price = double.Parse(product.productPrice.ToString());
            Number = 1;

        }
    }
}