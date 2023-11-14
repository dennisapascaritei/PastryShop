
namespace PastryShop.Dal.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);
            builder.Property(up => up.Name).IsRequired();
            builder.Property(up => up.Description).IsRequired();
            builder.Property(up => up.Price).IsRequired();
            builder.Property(up => up.Weight).IsRequired();
            builder.Property(up => up.ImageURL).IsRequired();
            builder.Property(up => up.CreatedDate).IsRequired();
            builder.Property(up => up.LastUpdated).IsRequired();
        }
    }
}
