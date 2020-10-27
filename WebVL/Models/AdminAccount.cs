using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebVL.Models
{
    public class AdminAccount
    {
        [Key]
        public string AdminId { get; set; }
        public string UserAdmin { get; set; }
        public string PassAdmin { get; set; }
        public string AdminName { get; set; }

    }
}