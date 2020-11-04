using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebVL.Models
{
    public class Order
    {
        [Key]
        public int IdBill { get; set; }
        public int IdCus { get; set; }
        public string Cargo { get; set; }
        public string Price { get; set; }
        public string Count { get; set; }
        public string Address { get; set; }
        public string NameCus { get; set; }
        public string DayBooks { get; set; }
        public int Status { get; set; }
        
    }
}