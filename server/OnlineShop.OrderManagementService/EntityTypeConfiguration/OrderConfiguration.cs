using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.OrderManagementService.Models;

namespace OnlineShop.OrderManagementService.EntityTypeConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Items).WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);
            builder.HasOne(x => x.Address).WithMany()
                .HasForeignKey(x => x.AddressId);
            builder.Property(x => x.RecipientFirstName).HasMaxLength(24);
            builder.Property(x => x.RecipientLastName).HasMaxLength(24);
            builder.Property(x => x.RecipientEmail).HasMaxLength(36);
            builder.Property(x => x.RecipientPhone).HasMaxLength(16);
        }
    }
}
