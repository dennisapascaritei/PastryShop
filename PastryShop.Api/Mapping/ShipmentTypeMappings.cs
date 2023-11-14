
using PastryShop.Domain.Aggregates.ShipmentTypeAggregate;

namespace PastryShop.Api.Mapping
{
    public class ShipmentTypeMappings : Profile
    {
        public ShipmentTypeMappings()
        {
            CreateMap<ShipmentType, ShipmentTypeResponse>();
            CreateMap<ShipmentTypeOrder, ShipmentTypeResponse>();
        }
    }
}
