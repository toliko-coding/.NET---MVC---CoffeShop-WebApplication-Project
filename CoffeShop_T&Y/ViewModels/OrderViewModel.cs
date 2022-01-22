using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using CoffeShop.Models;

namespace CoffeShop.ViewModels
{
    public class OrderViewModel
    {
        public ClientClass client { get; set; }
        
        public BaristasClass barista { get; set; }


        public CoffeClass coffe { get; set; }
        public List<CoffeClass> Products { get; set; }

        public TableClass Table { get; set; }
        public List<TableClass> Tables { get; set; }

        public ClientOrderClass order { get; set; }
        public List<ClientOrderClass> orders { get; set; }


    }

}