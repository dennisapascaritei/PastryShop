using PastryShop.Api.Contracts.LineItems.Response;

namespace PastryShop.Api.Mapping
{
    public class LineItemsMappings : Profile
    {
        public LineItemsMappings()
        {
            CreateMap<LineItem, LineItemResponse>();
        }
    }
}
