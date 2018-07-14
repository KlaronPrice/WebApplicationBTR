using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplicationBTR.Models
{
    public class ShopContext : DbContext
    {
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<ProductType> productTypes { get; set; }
        //public DbSet<TypeOfProduct> TypeOfProducts { get; set; }

        public ShopContext() : base("ShopContext")
        {
        }
    }
}