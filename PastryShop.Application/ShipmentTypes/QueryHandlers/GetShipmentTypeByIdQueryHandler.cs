
namespace PastryShop.Application.ShipmentTypes.QueryHandlers
{
    public class GetShipmentTypeByIdQueryHandler : IRequestHandler<GetShipmentTypeByIdQuery, OperationResult<ShipmentType>>
    {
        private readonly DataContext _ctx;

        public GetShipmentTypeByIdQueryHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<ShipmentType>> Handle(GetShipmentTypeByIdQuery request, CancellationToken cancellationToken)
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
