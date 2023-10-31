
namespace PastryShop.Application.Products.Queries
{
    public class GetProductByIdQuery : IRequest<OperationResult<Product>>
    {
        public Guid ProductId { get; set; }
    }
}
