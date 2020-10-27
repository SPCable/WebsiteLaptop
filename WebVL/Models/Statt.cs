using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebVL.Models
{
    public class Statt
    {
        [Key]
        public int Idpost { get; set; }
        public string Details { get; set; }
        public DateTime Daypost { get; set; }
        public string Postby { get; set; }
        public int  View { get; set; }

    }
}