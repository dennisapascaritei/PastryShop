
using PastryShop.Application.Orders.Commands;
using PastryShop.Application.Products;

namespace PastryShop.Application.Orders.CommandHandlers
{
    public class OrderCreateCommandHandler : IRequestHandler<OrderCreateCommand, OperationResult<Order>>
    {
        private readonly DataContext _ctx;
        public OrderCreateCommandHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<Order>> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Order>();

            try
            {
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
                
                var shippingAddressOrder = ShippingAddressOrder.CreateShippingAddressOrder(request.County, request.City, request.Address, request.PostCode);
                var newOrder = Order.CreateOrder(request.UserProfileId, productList, request.Price, request.ShipmentTypeId, shippingAddressOrder, request.UserInstructions, request.DeliveryDate);

                _ctx.Orders.Add(newOrder);
                await _ctx.SaveChangesAsync(cancellationToken);

                result.Payload = newOrder;

            }
            catch (Exception ex)
            {
                result.AddUnknownError(ex.Message);
            }
            
            return result;
        }
    }
}
