using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebVL.Models
{
    public class ShoppingCard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public double Price { get; set; }
        public int Number { get; set; }
        public double Total { get { return Number * Price;  } }
    }
}