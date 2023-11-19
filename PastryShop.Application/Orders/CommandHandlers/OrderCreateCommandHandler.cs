
namespace PastryShop.Application.Orders.CommandHandlers
{
    public class OrderCreateCommandHandler : IRequestHandler<OrderCreateCommand, OperationResult<Order>>
    {
        private readonly DataContext _ctx;
        private readonly OperationResult<Order> _result = new OperationResult<Order>();
        public OrderCreateCommandHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<Order>> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var shipmentTypeOrder = CreateShipmentTypeAsync(request, cancellationToken).Result;
                var shippingAddressOrder = ShippingAddressOrder.CreateShippingAddressOrder(request.County, request.City, request.Address, request.PostCode);
                var newOrder = Order.CreateOrder(request.UserProfileId, request.Price, shipmentTypeOrder, shippingAddressOrder, request.UserInstructions, request.DeliveryDate);

                await CreateLineItemsAsync(request.ProductList, newOrder, cancellationToken);

                _ctx.Orders.Add(newOrder);
                await _ctx.SaveChangesAsync();

                _result.Payload = newOrder;

            }
            catch (Exception ex)
            {
                _result.AddUnknownError(ex.Message);
            }
            
            return _result;
        }

        private async Task CreateLineItemsAsync(List<Guid> productIds, Order order, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var productId in productIds)
                {
                    var product = await _ctx.Products.FirstOrDefaultAsync(pr => pr.ProductId == productId, cancellationToken);

                    if (product == null)
                    {
                        _result.AddError(ErrorCode.NotFound, string.Format(ProductErrorMessages.ProductNotFound, productId));
                    }
                    else
                    {
                        order.AddLineItem(product, order.OrderId);
                    } 
                        
                }

                if (order.LineItems.Count == 0)
                {
                    _result.AddError(ErrorCode.NotFound, OrderErrorMessages.OrderHasNoValidProductsAdded);
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
        private async Task<ShipmentTypeOrder> CreateShipmentTypeAsync(OrderCreateCommand order, CancellationToken cancellationToken)
        {
            try
            {
                var shipmentType = await _ctx.ShipmentTypes.FirstOrDefaultAsync(st => st.ShipmentTypeId == order.ShipmentTypeId);
                if (shipmentType is null)
                {
                    _result.AddError(ErrorCode.NotFound, string.Format(ShipmentTypeErrorMessages.ShipmentTypeNotFound, order.ShipmentTypeId));
                }
                var shipmentTypeOrder = ShipmentTypeOrder.CreateShipmentTypeOrder(shipmentType.Name, shipmentType.Price);

                return shipmentTypeOrder;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
