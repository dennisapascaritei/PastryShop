
namespace PastryShop.Application.Products.Commands
{
    public class ProductCreateCommand : IRequest<OperationResult<Product>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
        public string ImageURL { get; set; }
    }
}
