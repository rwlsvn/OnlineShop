using Microsoft.EntityFrameworkCore;
using OnlineShop.ProductManagementService.Models;

namespace OnlineShop.ProductManagementService.Data
{
    public interface IProductsDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellation = default);
    }
}
