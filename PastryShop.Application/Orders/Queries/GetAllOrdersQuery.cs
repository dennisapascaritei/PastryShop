
namespace PastryShop.Application.Orders.Queries
{
    public class GetAllOrdersQuery : IRequest<OperationResult<List<Order>>>
    {
        public Guid UserProfileId { get; set; }
    }
}
