using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoffeShop.Models
{
    public class Order
    {
        public string Barista { get; set; }

        public string ClientID { get; set; }

        public string TableID { get; set; }

        public string TotalPrice { get; set; }

        [Key]
        public string items { get; set; }

        

    }
}