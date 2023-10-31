
namespace PastryShop.Application.ShipmentTypes.Queries
{
    public class GetShipmentTypeByIdQuery : IRequest<OperationResult<ShipmentType>>
    {
        public Guid ShipmentTypeId { get; set; }
    }
}
