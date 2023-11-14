
using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using PastryShop.Application.Orders.Commands;
using PastryShop.Application.Products;
using PastryShop.Application.ShipmentTypes;
using PastryShop.Domain.Aggregates.OrderAggregate;
using PastryShop.Domain.Aggregates.ShipmentTypeAggregate;
using System.ComponentModel;

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
                await using var transaction = await _ctx.Database.BeginTransactionAsync(cancellationToken);

                var shipmentTypeOrder = CreateShipmentTypeAsync(request, transaction, cancellationToken).Result;
                var shippingAddressOrder = ShippingAddressOrder.CreateShippingAddressOrder(request.County, request.City, request.Address, request.PostCode);
                var newOrder = Order.CreateOrder(request.UserProfileId, request.Price, shipmentTypeOrder, shippingAddressOrder, request.UserInstructions, request.DeliveryDate);

                CreateLineItemsAsync(request.ProductList, newOrder, transaction, cancellationToken);

                _ctx.Orders.Add(newOrder);
                await _ctx.SaveChangesAsync(cancellationToken);

                _result.Payload = newOrder;

            }
            catch (Exception ex)
            {
                _result.AddUnknownError(ex.Message);
            }
            
            return _result;
        }

        private async Task CreateLineItemsAsync(List<Guid> productIds, Order order, IDbContextTransaction transaction, CancellationToken cancellationToken)
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
                        order.AddLineItem(product);
                    } 
                        
                }

                if (order.LineItems.Count == 0)
                {
                    _result.AddError(ErrorCode.NotFound, OrderErrorMessages.OrderHasNoValidProductsAdded);
                    transaction.RollbackAsync(cancellationToken);
                }
            }
            catch(Exception)
            {
                transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
        private async Task<ShipmentTypeOrder> CreateShipmentTypeAsync(OrderCreateCommand order, IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            try
            {
                var shipmentType = await _ctx.ShipmentTypes.FirstOrDefaultAsync(st => st.ShipmentTypeId == order.ShipmentTypeId);
                if (shipmentType is null)
                {
                    _result.AddError(ErrorCode.NotFound, string.Format(ShipmentTypeErrorMessages.ShipmentTypeNotFound, order.ShipmentTypeId));
                    _ctx.Database.RollbackTransactionAsync(cancellationToken);
                }
                var shipmentTypeOrder = ShipmentTypeOrder.CreateShipmentTypeOrder(shipmentType.Name, shipmentType.Price);

                return shipmentTypeOrder;
            }
            catch (Exception)
            {
                _ctx.Database.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
