
using PastryShop.Domain.Aggregates.ShipmentTypeAggregate;

namespace PastryShop.Application.ShipmentTypes.Commands
{
    public class CreateShipmentTypeCommand : IRequest<OperationResult<ShipmentType>>
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
