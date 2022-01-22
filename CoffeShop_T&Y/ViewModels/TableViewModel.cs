using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoffeShop.ViewModels
{
    public class TableViewModel
    {
        public TableClass Table { get; set; }


        public List<TableClass> Tables { get; set; }
    }
}
