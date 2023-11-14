
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PastryShop.Dal.Configuration
{
    internal class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.OrderId);
            builder.HasOne<UserProfile>()
                .WithMany()
                .HasForeignKey(o => o.UserProfileId)
                .IsRequired();
            builder.Property(o => o.Price);
            builder.HasMany(o => o.LineItems)
                .WithOne()
                .HasForeignKey(li => li.OrderId);
            builder.OwnsOne(bi => bi.ShippingAddress, shippingAddressBuilder =>
            {
                shippingAddressBuilder.Property(sa => sa.County).IsRequired().HasMaxLength(40);
                shippingAddressBuilder.Property(sa => sa.City).IsRequired().HasMaxLength(40);
                shippingAddressBuilder.Property(sa => sa.Address).IsRequired().HasMaxLength(40);
                shippingAddressBuilder.Property(sa => sa.PostCode).IsRequired().HasMaxLength(40);
            });
            builder.OwnsOne(bi => bi.ShipmentType, shipmentTypeBuilder =>
            {
                shipmentTypeBuilder.Property(sa => sa.Name).IsRequired().HasMaxLength(40);
                shipmentTypeBuilder.Property(sa => sa.Price).IsRequired().HasMaxLength(40);
                shipmentTypeBuilder.Property(sa => sa.LastUpdated).IsRequired().HasMaxLength(40);
            });

            builder.Property(sa => sa.UserInstructions).IsRequired().HasMaxLength(200);
            builder.Property(sa => sa.DateCreated).IsRequired().HasMaxLength(40);
            builder.Property(sa => sa.DeliveryDate).IsRequired().HasMaxLength(40);
        }
    }
}
