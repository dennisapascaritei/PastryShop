
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PastryShop.Dal.Configuration
{
    internal class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.OrderId);
            builder.OwnsOne(x => x.ShippingAddressOrder);
        }
    }
}
