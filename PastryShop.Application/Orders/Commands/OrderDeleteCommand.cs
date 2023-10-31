
namespace PastryShop.Application.Orders.Commands
{
    public class OrderDeleteCommand : IRequest<OperationResult<Order>>
    {
        public Guid OrderId { get; set; }
        public Guid UserProfileId { get; set; }
    }
}
