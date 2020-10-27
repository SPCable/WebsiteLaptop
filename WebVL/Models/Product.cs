using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.SqlServer.Server;

namespace WebVL.Models
{
    public class Product
    {
        [Key]
        public string ProductId { get; set; }
        public string productName { get; set; }
        public double productPrice { get; set; }

        public double productDiscout { get; set; }

        public string productView { get; set; }

        public string productDesc { get; set; }

        public string productImg { get; set; }
        public string productImg1 { get; set; }
        public string productImg2 { get; set; }
        public string productImg3 { get; set; }

        public int productEvaluate { get; set; }

        public int productAmount { get; set; }
        public string CategoryId { get; set; }

    }
}