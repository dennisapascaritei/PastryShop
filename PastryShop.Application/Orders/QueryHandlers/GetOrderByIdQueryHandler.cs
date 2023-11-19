
using Microsoft.EntityFrameworkCore;
using PastryShop.Application.Orders.Queries;

namespace PastryShop.Application.Orders.QueryHandlers
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OperationResult<Order>>
    {
        private readonly DataContext _ctx;

        public GetOrderByIdQueryHandler(DataContext ctx)
        {
          _ctx = ctx;
        }
        public async Task<OperationResult<Order>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Order>();

            try
            {
                var order = await _ctx.Orders
                    .Include(o => o.LineItems)
                    .Include(o => o.ShipmentType)
                    .Include(o => o.ShippingAddress)
                    .FirstOrDefaultAsync(o => o.OrderId == request.OrderId, cancellationToken);

                if (order is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(OrderErrorMessages.OrderNotFound, request.OrderId));
                    return result;
                }

                result.Payload = order;

            }
            catch (Exception ex)
            {
                result.AddUnknownError(ex.Message);
            }

            return result;
        }
    }
}
