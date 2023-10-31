
namespace PastryShop.Application.Mappings
{
    public class ProductMappings : Profile
    {
        public ProductMappings()
        {
            CreateMap<ProductUpdateCommand, Product>();
        }
    }
}
