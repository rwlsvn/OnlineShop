using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.OrderManagementService.Models;

namespace OnlineShop.OrderManagementService.EntityTypeConfiguration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Order).WithMany(x => x.Items)
                .HasForeignKey(x => x.OrderId);
            builder.Property(x => x.ProductName).HasMaxLength(256);
            builder.Property(x => x.ProductPrice).HasPrecision(18, 2);
        }
    }
}
