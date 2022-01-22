using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CoffeShop.Models;

namespace CoffeShop.Dal
{
    public class BaristasDal : DbContext
    {
        public DbSet<BaristasClass> Baristases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BaristasClass>().ToTable("BaristasTBL");
        }
    }
}