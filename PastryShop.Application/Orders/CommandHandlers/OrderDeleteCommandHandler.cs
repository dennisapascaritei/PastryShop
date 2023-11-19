
using PastryShop.Application.Orders.Commands;
using System.Collections.Immutable;

namespace PastryShop.Application.Orders.CommandHandlers
{
    public class OrderDeleteCommandHandler : IRequestHandler<OrderDeleteCommand, OperationResult<Order>>
    {
        private readonly DataContext _ctx;
        public OrderDeleteCommandHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<Order>> Handle(OrderDeleteCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Order>();

            try
            {
                var order = await _ctx.Orders.FirstOrDefaultAsync(o => o.OrderId == request.OrderId, cancellationToken);

                if (order is null)
                {
                    result.AddError(ErrorCode.NotFound, OrderErrorMessages.OrderNotFound);
                    return result;
                }

                if (order.UserProfileId != request.UserProfileId)
                {
                    result.AddError(ErrorCode.OrderDeleteNotPossible, OrderErrorMessages.OrderDeleteNotPossible);
                    return result;
                }

                order.EmptyLineItemstList();

                _ctx.Orders.Remove(order);
                await _ctx.SaveChangesAsync(cancellationToken);

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
