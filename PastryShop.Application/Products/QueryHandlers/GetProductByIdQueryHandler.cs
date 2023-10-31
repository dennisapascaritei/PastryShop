
using Microsoft.EntityFrameworkCore;
using PastryShop.Application.Products.Queries;

namespace PastryShop.Application.Products.QueryHandlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, OperationResult<Product>>
    {
        private readonly DataContext _ctx;

        public GetProductByIdQueryHandler(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<OperationResult<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Product>();

            try
            {
                var product = await _ctx.Products.FirstOrDefaultAsync(pr => pr.ProductId == request.ProductId);

                if (product == null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ProductErrorMessages.ProductNotFound, request.ProductId));
                }

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
