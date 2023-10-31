
namespace PastryShop.Application.ShipmentTypes.Commands
{
    public class DeleteShipmentTypeCommand : IRequest<OperationResult<ShipmentType>>
    {
        public Guid ShipmentTypeId { get; set; }
    }
}
