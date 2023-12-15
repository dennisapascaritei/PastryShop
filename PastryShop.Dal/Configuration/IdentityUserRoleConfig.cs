
namespace PastryShop.Dal.Configuration
{
    internal class IdentityUserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasKey(iur => new { iur.UserId, iur.RoleId });
            
        }
    }
}
