
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PastryShop.Dal.Configuration
{
    internal class UserProfileConfig : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(up => up.UserProfileId);
            builder.OwnsOne(up => up.BasicInfo, basicInfoBuilder =>
            {
                basicInfoBuilder.Property(bi => bi.FirstName).IsRequired().HasMaxLength(40);
                basicInfoBuilder.Property(bi => bi.LastName).IsRequired().HasMaxLength(40);
                basicInfoBuilder.Property(bi => bi.EmailAddress).IsRequired().HasMaxLength(255);
                basicInfoBuilder.Property(bi => bi.Phone).IsRequired().HasMaxLength(15);
                basicInfoBuilder.OwnsOne(bi => bi.ShippingAddress, shippingAddressBuilder =>
                {
                    shippingAddressBuilder.Property(sa => sa.County).IsRequired().HasMaxLength(40);
                    shippingAddressBuilder.Property(sa => sa.City).IsRequired().HasMaxLength(40);
                    shippingAddressBuilder.Property(sa => sa.Address).IsRequired().HasMaxLength(40);
                    shippingAddressBuilder.Property(sa => sa.PostCode).IsRequired().HasMaxLength(40);
                });
            });
            builder.Property(up => up.DateCreated).IsRequired();
            builder.Property(up => up.DateUpdated).IsRequired();

            //builder.HasIndex(up => up.BasicInfo.EmailAddress).IsUnique();
            
        }
    }
}
