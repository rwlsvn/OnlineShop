using Microsoft.EntityFrameworkCore;
using OnlineShop.OrderManagementService.Models;

namespace OnlineShop.OrderManagementService.Data
{
    public interface IOrderDbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellation = default);
    }
}
