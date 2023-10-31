
namespace PastryShop.Application.Orders.Queries
{
    public class GetOrderByIdQuery : IRequest<OperationResult<Order>>
    {
        public Guid OrderId { get; set; }
    }
}
