
namespace PastryShop.Api.Mapping
{
    public class ProductMappings : Profile
    {
        public ProductMappings()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<LineItem, ProductResponse>();
        }
        
    }
}
