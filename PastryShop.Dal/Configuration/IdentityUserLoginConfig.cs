
namespace PastryShop.Dal.Configuration
{
    internal class IdentityUserLoginConfig : IEntityTypeConfiguration<IdentityUserLogin<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
        {
            builder.HasKey(iul => iul.UserId);
        }
    }
}
