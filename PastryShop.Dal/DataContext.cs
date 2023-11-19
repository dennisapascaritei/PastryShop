
using PastryShop.Domain.Aggregates.ShipmentTypeAggregate;

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
        public DbSet<LineItem> LineItem { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserProfileConfig());
            builder.ApplyConfiguration(new LineItemConfig());
            builder.ApplyConfiguration(new OrderConfig());
            builder.ApplyConfiguration(new ProductConfiguration());

            builder.ApplyConfiguration(new IdentityUserLoginConfig());
            builder.ApplyConfiguration(new IdentityUserRoleConfig());
            builder.ApplyConfiguration(new IdentityUserTokenConfig());
        }
    }
}
