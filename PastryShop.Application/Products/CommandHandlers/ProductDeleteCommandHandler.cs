
using Microsoft.EntityFrameworkCore;

namespace PastryShop.Application.Products.CommandHandlers
{
    public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommand, OperationResult<Product>>
    {
        private readonly DataContext _ctx;
        public ProductDeleteCommandHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<Product>> Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Product>();
            try
            {
                var product = await _ctx.Products.FirstOrDefaultAsync(pr => pr.ProductId == request.ProductId, cancellationToken);
                if (product is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ProductErrorMessages.ProductNotFound, request.ProductId));
                }

                _ctx.Products.Remove(product);
                await _ctx.SaveChangesAsync(cancellationToken);

                result.Payload = product;
            }
            catch(Exception ex)
            {
                result.AddUnknownError(ex.Message);
            }

            return result;
        }
    }
}
