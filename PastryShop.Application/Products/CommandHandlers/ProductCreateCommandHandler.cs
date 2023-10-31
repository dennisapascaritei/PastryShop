
namespace PastryShop.Application.Products.CommandHandlers
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, OperationResult<Product>>
    {
        private readonly DataContext _ctx;
        public ProductCreateCommandHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<Product>> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Product>();

            try
            {
                var product = Product.CreateProduct(request.Name, request.Description, request.Price, request.Weight, request.ImageURL);
                _ctx.Products.Add(product);
                await _ctx.SaveChangesAsync(cancellationToken);

                result.Payload = product;
            }
            catch (Exception ex)
            {
                result.AddUnknownError(ex.Message);
            }


            return result;
        }
    }
}
