
using PastryShop.Application.Orders.Queries;

namespace PastryShop.Application.Orders.QueryHandlers
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, OperationResult<List<Order>>>
    {
        private readonly DataContext _ctx;
        public GetAllOrdersQueryHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<List<Order>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<Order>>();

            try
            {
                var orders = await _ctx.Orders
                    .Include(o => o.LineItems)
                    .Include(o => o.ShipmentType)
                    .Include(o => o.ShippingAddress)
                    .ToListAsync(cancellationToken);
                
                if (orders.Count == 0)
                {
                    result.AddError(ErrorCode.NotFound, OrderErrorMessages.OrderListIsEmpty);
                    return result;
                }

                result.Payload = orders;
            }
            catch (Exception ex)
            {
                result.AddUnknownError(ex.Message);
            }

            return result;
        }
    }
}
