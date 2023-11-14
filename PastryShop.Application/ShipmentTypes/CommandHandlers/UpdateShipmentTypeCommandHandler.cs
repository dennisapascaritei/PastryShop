
using PastryShop.Domain.Aggregates.ShipmentTypeAggregate;

namespace PastryShop.Application.ShipmentTypes.CommandHandlers
{
    public class UpdateShipmentTypeCommandHandler : IRequestHandler<UpdateShipmentTypeCommand, OperationResult<ShipmentType>>
    {
        private readonly DataContext _ctx;

        public UpdateShipmentTypeCommandHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<ShipmentType>> Handle(UpdateShipmentTypeCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<ShipmentType>();

            try
            {
                var shipmentType = await _ctx.ShipmentTypes.FirstOrDefaultAsync(st => st.ShipmentTypeId == request.ShipmentTypeId, cancellationToken);

                if (shipmentType is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ShipmentTypeErrorMessages.ShipmentTypeNotFound, request.ShipmentTypeId));
                    return result;
                }
                 
                var newShipmentType = ShipmentType.CreateShipmentType(request.Name, request.Price);
                shipmentType.UpdateShipmentType(newShipmentType);

                _ctx.ShipmentTypes.Update(shipmentType);
                await _ctx.SaveChangesAsync(cancellationToken);

                result.Payload = shipmentType;
            }
            catch (Exception ex)
            {
                result.AddUnknownError(ex.Message);
            }

            return result;
        }
    }
}
