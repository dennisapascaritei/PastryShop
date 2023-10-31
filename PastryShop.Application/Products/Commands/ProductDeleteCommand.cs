
namespace PastryShop.Application.Products.Commands
{
    public class ProductDeleteCommand : IRequest<OperationResult<Product>>
    {
        public Guid ProductId { get; set; }
    }
}
