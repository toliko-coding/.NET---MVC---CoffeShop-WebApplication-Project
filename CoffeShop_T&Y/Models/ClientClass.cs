using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoffeShop.Models
{
    public class ClientClass
    {

        [Key]
        [Required(ErrorMessage = "You Must Input your Phone Number")]
        
        public string PhoneNumber { get; set; }

        public string Name { get; set; }

        

        [Required(ErrorMessage = "You Must Input your PasswordSSS")]
        public string Password { get; set; }

    }
}