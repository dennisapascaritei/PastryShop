namespace PastryShop.Api.Mapping
{
    public class OrderMappings : Profile
    {
        public OrderMappings()
        {
            CreateMap<Order, OrderResponse>();
        }
    }
}
