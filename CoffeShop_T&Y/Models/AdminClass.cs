using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoffeShop.Models
{
    public class AdminClass
    {
        [Key]
        [Required(ErrorMessage = "You Must Input your ADMIN ID")]
        public string ID { get; set; }

        [Required(ErrorMessage = "You Must Input your Password")]
        public string Password { get; set; }
    }
}