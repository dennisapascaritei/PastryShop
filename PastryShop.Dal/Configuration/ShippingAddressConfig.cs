
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PastryShop.Dal.Configuration
{
    internal class ShippingAddressConfig : IEntityTypeConfiguration<ShippingAddress>
    {
        public void Configure(EntityTypeBuilder<ShippingAddress> builder)
        {
            
        }
    }
}
