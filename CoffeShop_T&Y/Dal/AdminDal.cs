using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoffeShop.Dal
{
    public class AdminDal : DbContext
    {
        public DbSet<AdminClass> Admins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AdminClass>().ToTable("Admin");
        }

    }
}