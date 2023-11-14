namespace PastryShop.Api.Mapping
{
    public class OrderMappings : Profile
    {
        public OrderMappings()
        {
            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.ProductList, opt =>
                opt.MapFrom(src => src.LineItems))
                .ForMember(dest => dest.Price, opt =>
                opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.ShipmentType, opt =>
                opt.MapFrom(src => src.ShipmentType))
                .ForMember(dest => dest.ShippingAddressOrder, opt =>
                opt.MapFrom(src => src.ShippingAddress))
                .ForMember(dest => dest.UserInstructions, opt =>
                opt.MapFrom(src => src.UserInstructions));
        }
    }
}
