
using Microsoft.EntityFrameworkCore.Storage;
using PastryShop.Application.Orders.Commands;
using PastryShop.Application.Products;
using PastryShop.Application.ShipmentTypes;

namespace PastryShop.Application.Orders.CommandHandlers
{
    public class OrderUpdateCommandHandler : IRequestHandler<OrderUpdateCommand, OperationResult<Order>>
    {
        private readonly DataContext _ctx;
        private readonly OperationResult<Order> _result = new OperationResult<Order>();
        public OrderUpdateCommandHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<Order>> Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _ctx.Orders
                    .Include(o => o.LineItems)
                    .Include(o => o.ShipmentType)
                    .Include(o => o.ShippingAddress)
                    .FirstOrDefaultAsync(o => o.OrderId == request.OrderId, cancellationToken);

                if (order is null)
                {
                    _result.AddError(ErrorCode.NotFound, OrderErrorMessages.OrderNotFound);
                    return _result;
                }

                if (order.UserProfileId != request.UserProfileId)
                {
                    _result.AddError(ErrorCode.OrderUpdateNotPossible, OrderErrorMessages.OrderUpdateNotPossible);
                    return _result;
                }

                var shipmentTypeOrder = CreateShipmentTypeAsync(request, cancellationToken).Result;
                var shippingAddressOrder = ShippingAddressOrder.CreateShippingAddressOrder(request.County, request.City, request.Address, request.PostCode);
                var updatedOrder = Order.CreateOrder(request.UserProfileId, request.Price, shipmentTypeOrder, shippingAddressOrder, request.UserInstructions, request.DeliveryDate);

                await CreateLineItemsAsync(request.ProductList, updatedOrder, cancellationToken);

                //order.EmptyLineItemstList();
                order.UpdateOrder(updatedOrder);

                _ctx.Orders.Update(order);
                await _ctx.SaveChangesAsync(cancellationToken);

                _result.Payload = updatedOrder;

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
                    else order.AddLineItem(product, order.OrderId);
                }

                if (order.LineItems.Count == 0)
                {
                    _result.AddError(ErrorCode.NotFound, OrderErrorMessages.OrderHasNoValidProductsAdded);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<ShipmentTypeOrder> CreateShipmentTypeAsync(OrderUpdateCommand order, CancellationToken cancellationToken)
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
