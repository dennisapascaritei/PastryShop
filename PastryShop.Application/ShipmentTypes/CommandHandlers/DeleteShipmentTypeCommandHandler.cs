
using Microsoft.EntityFrameworkCore;

namespace PastryShop.Application.ShipmentTypes.CommandHandlers
{
    public class DeleteShipmentTypeCommandHandler : IRequestHandler<DeleteShipmentTypeCommand, OperationResult<ShipmentType>>
    {
        private readonly DataContext _ctx;

        public DeleteShipmentTypeCommandHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<ShipmentType>> Handle(DeleteShipmentTypeCommand request, CancellationToken cancellationToken)
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

                _ctx.ShipmentTypes.Remove(shipmentType);
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
