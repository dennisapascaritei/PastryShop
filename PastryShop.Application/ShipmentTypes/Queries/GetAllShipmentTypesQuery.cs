
using PastryShop.Domain.Aggregates.ShipmentTypeAggregate;

namespace PastryShop.Application.ShipmentTypes.Queries
{
    public class GetAllShipmentTypesQuery : IRequest<OperationResult<List<ShipmentType>>>
    {
    }
}
