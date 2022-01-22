using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace CoffeShop.Models
{
    public class CoffeClass
    {
        [Key]
        [Required(ErrorMessage ="ee")]
        public string ID { get; set; }
        [Required(ErrorMessage = "ee")]
        public string CoffeName { get; set; }

        [Required(ErrorMessage = "ee")]
        public int Price { get; set; }

        [Required(ErrorMessage = "ee")]
        public int BPrice { get; set; }

        [Required(ErrorMessage = "ee")]
        public int AgeLimit { get; set; }

        [Required(ErrorMessage = "ee")]
        public int avlilable { get; set; }

        [Required(ErrorMessage = "ee")]
        public string imgName { get; set; }

        [Required(ErrorMessage = "ee")]
        public int isBusiness { get; set; }



    }
}