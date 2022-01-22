using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoffeShop.Dal 
{
    public class CoffeDal : DbContext
    {
        public DbSet<CoffeClass> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CoffeClass>().ToTable("ProductsTBL");
        }
    }
}