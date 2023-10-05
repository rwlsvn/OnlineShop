using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ProductManagementService.Models;

namespace OnlineShop.ProductManagementService.EntityTypeConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Category).WithMany(x => x.Products)
                .HasForeignKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(256);
            builder.Property(x => x.Description).HasMaxLength(1024);
            builder.Property(x => x.Price).HasPrecision(18, 2);
            builder.Property(x => x.ImageName).HasMaxLength(42);
        }
    }
}
