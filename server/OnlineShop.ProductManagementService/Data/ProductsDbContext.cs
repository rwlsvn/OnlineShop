using Microsoft.EntityFrameworkCore;
using OnlineShop.ProductManagementService.Models;
using System.Reflection;

namespace OnlineShop.ProductManagementService.Data
{
    public class ProductsDbContext : DbContext, IProductsDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
