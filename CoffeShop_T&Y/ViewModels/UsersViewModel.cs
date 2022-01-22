using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoffeShop.Models;


namespace CoffeShop.ViewModels
{
    public class UsersViewModel
    {
        public ClientClass client { get; set; }
        public List<ClientClass> clients { get; set; }

        public BaristasClass barista { get; set; }
        public List<BaristasClass> baristaS { get; set; }
    }
}