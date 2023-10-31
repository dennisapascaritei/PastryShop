
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace PastryShop.Application.ShipmentTypes.QueryHandlers
{
    public class GetAllShipmentTypesQueryHandler : IRequestHandler<GetAllShipmentTypesQuery, OperationResult<List<ShipmentType>>>
    {
        private readonly DataContext _ctx;

        public GetAllShipmentTypesQueryHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<List<ShipmentType>>> Handle(GetAllShipmentTypesQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<ShipmentType>>();

            try
            {
                var shipmentTypes = await _ctx.ShipmentTypes.ToListAsync(cancellationToken);

                if (shipmentTypes is null)
                {
                    result.AddError(ErrorCode.NotFound, ShipmentTypeErrorMessages.ShipmentTypeListIsEmpty);
                    return result;
                }

                result.Payload = shipmentTypes;
            }
            catch (Exception ex)
            {
                result.AddUnknownError(ex.Message);
            }

            return result;
        }
    }
}
