using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.OrderManagementService.Models;

namespace OnlineShop.OrderManagementService.EntityTypeConfiguration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Country).HasMaxLength(36);
            builder.Property(x => x.City).HasMaxLength(36);
            builder.Property(x => x.StreetAddresss).HasMaxLength(64);
            builder.Property(x => x.PostalCode).HasMaxLength(12);
        }
    }
}
