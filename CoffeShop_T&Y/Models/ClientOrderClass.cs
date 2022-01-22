using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace CoffeShop.Models
{
    public class ClientOrderClass
    {
        
        public string ClientID { get; set; }

        public string TableID { get; set; }

        public string TotalPrice { get; set; }

        public string items { get; set; }

        public List<CoffeClass> CoffeList { get; set; }


    }
}