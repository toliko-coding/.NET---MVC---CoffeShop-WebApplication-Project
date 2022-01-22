using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace CoffeShop.ViewModels
{
    public class CoffeViewModel
    {
       
        public CoffeClass coffe { get; set; }

        
        public List<CoffeClass> Products { get; set; }
    }
}