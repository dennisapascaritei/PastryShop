
namespace PastryShop.Application.Products.Commands
{
    public class ProductUpdateCommand : IRequest<OperationResult<Product>>
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public string ImageURL { get; set; }
    }
}
