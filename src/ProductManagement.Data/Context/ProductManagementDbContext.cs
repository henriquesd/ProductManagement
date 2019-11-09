using Microsoft.EntityFrameworkCore;
using ProductManagement.Business.Models;

namespace ProductManagement.Data.Context
{
    public class ProductManagementDbContext : DbContext
    {
        public ProductManagementDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductManagementDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
