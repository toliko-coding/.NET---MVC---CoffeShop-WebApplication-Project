using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

using System.Web;

namespace CoffeShop.Models
{
    public class BaristasClass
    {
       [Key]
       [Required(ErrorMessage = "You Must Enter your ID")]
        public string ID { get; set; }

        
        [Required(ErrorMessage = "You Must Enter your PASSWORD")]
        public string Password { get; set; }

        
        public string Name { get; set; }
    }
}