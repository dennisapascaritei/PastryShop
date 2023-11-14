
using PastryShop.Domain.Aggregates.ShipmentTypeAggregate;

namespace PastryShop.Application.ShipmentTypes.Commands
{
    public class UpdateShipmentTypeCommand : IRequest<OperationResult<ShipmentType>>
    {
        public Guid ShipmentTypeId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
