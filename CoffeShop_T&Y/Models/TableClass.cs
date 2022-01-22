using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace CoffeShop.Models
{
    public class TableClass
    {
      [Key]
        public string ID { get; set; }

        public string Seats { get; set; }

        public string Dicription { get; set; }

        public string Free { get; set; }

        public string Inside { get; set; }


    }
}




