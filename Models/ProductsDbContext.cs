using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApiProducts.Models
{
    public class ProductsDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ProductsDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // HiLo Id generation For all the entities
            //modelBuilder.UseHiLo();

        }
    }
}
