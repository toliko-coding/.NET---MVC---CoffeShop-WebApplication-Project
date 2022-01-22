using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoffeShop.Dal
{
    public class ClientDal : DbContext
    {
        public DbSet<ClientClass> Clients { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ClientClass>().ToTable("ClientTBL");
        }
    }
}