
namespace PastryShop.Dal
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ShipmentType> ShipmentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserProfileConfig());
            builder.ApplyConfiguration(new ShippingAddressConfig());

            builder.ApplyConfiguration(new IdentityUserLoginConfig());
            builder.ApplyConfiguration(new IdentityUserRoleConfig());
            builder.ApplyConfiguration(new IdentityUserTokenConfig());
        }
    }
}
