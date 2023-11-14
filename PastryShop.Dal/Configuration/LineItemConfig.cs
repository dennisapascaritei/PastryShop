using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastryShop.Dal.Configuration
{
    internal class LineItemConfig : IEntityTypeConfiguration<LineItem>
    {
        public void Configure(EntityTypeBuilder<LineItem> builder)
        {
            builder.HasKey(i => i.LineItemId);
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(i => i.ProductId);
        }
    }
}
