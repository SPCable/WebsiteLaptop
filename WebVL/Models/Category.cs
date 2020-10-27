using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebVL.Models
{
    public class Category
    {
        public string categoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImg { get; set; }
        public string BrandId { get; set; }
        public string BrandName { get; set; }
    }
}