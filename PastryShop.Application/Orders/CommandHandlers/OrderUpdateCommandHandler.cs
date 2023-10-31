
using PastryShop.Application.Orders.Commands;
using PastryShop.Application.Products;

namespace PastryShop.Application.Orders.CommandHandlers
{
    public class OrderUpdateCommandHandler : IRequestHandler<OrderUpdateCommand, OperationResult<Order>>
    {
        private readonly DataContext _ctx;
        public OrderUpdateCommandHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<Order>> Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Order>();

            try
            {
                var order = await _ctx.Orders
                    .Include(o => o.ProductList)
                    .Include(o => o.ShipmentType)
                    .Include(o => o.ShippingAddressOrder)
                    .FirstOrDefaultAsync(o => o.OrderId == request.OrderId);

                if (order is null)
                {
                    result.AddError(ErrorCode.NotFound, OrderErrorMessages.OrderNotFound);
                    return result;
                }

                var productList = new List<Product>();
                foreach (var productId in request.ProductList)
                {
                    var product = await _ctx.Products.FirstOrDefaultAsync(pr => pr.ProductId == productId);

                    if (product == null)
                    {
                        result.AddError(ErrorCode.NotFound, string.Format(ProductErrorMessages.ProductNotFound, productId));
                    }
                    else productList.Add(product);
                }

                if (productList.Count == 0)
                {
                    result.AddError(ErrorCode.NotFound, OrderErrorMessages.OrderHasNoValidProductsAdded);
                    return result;
                }

                if (order.UserProfileId != request.UserProfileId)
                {
                    result.AddError(ErrorCode.OrderUpdateNotPossible, OrderErrorMessages.OrderUpdateNotPossible);
                    return result;
                }

                var shippingAddressOrder = ShippingAddressOrder.CreateShippingAddressOrder(request.County, request.City, request.Address, request.PostCode);
                var updatedOrder = Order.CreateOrder(request.UserProfileId, productList, request.Price, request.ShipmentTypeId, shippingAddressOrder, request.UserInstructions, request.DeliveryDate);
                
                order.UpdateOrder(updatedOrder);

                _ctx.Orders.Update(order);
                await _ctx.SaveChangesAsync(cancellationToken);

                result.Payload = updatedOrder;

            }
            catch (Exception ex)
            {
                result.AddUnknownError(ex.Message);
            }

            return result;
        }
    }
}
