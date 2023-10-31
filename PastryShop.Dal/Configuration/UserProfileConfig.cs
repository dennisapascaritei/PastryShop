
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PastryShop.Dal.Configuration
{
    internal class UserProfileConfig : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.OwnsOne(up => up.BasicInfo);
            
        }
    }
}
