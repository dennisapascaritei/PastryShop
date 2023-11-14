
using PastryShop.Domain.Aggregates.ShipmentTypeAggregate;

namespace PastryShop.Application.ShipmentTypes.CommandHandlers
{
    public class CreateShipmentTypeCommandHandler : IRequestHandler<CreateShipmentTypeCommand, OperationResult<ShipmentType>>
    {
        private readonly DataContext _ctx;

        public CreateShipmentTypeCommandHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<ShipmentType>> Handle(CreateShipmentTypeCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<ShipmentType>();

            try
            {
                var shipmentType = ShipmentType.CreateShipmentType(request.Name, request.Price);

                _ctx.ShipmentTypes.Add(shipmentType);
                await _ctx.SaveChangesAsync(cancellationToken);

                result.Payload = shipmentType;
            }
            catch(Exception ex)
            {
                result.AddUnknownError(ex.Message);
            }

            return result;

        }
    }
}
