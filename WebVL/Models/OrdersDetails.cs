using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebVL.Models
{
    public class OrdersDetails
    {
        public int id { get; set; }

        public int OrderId { get; set; }
        public string ProductId { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }
    }
}