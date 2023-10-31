
namespace PastryShop.Api.Mapping
{
    public class ShipmentTypeMappings : Profile
    {
        public ShipmentTypeMappings()
        {
            CreateMap<ShipmentType, ShipmentTypeResponse>();
        }
    }
}
