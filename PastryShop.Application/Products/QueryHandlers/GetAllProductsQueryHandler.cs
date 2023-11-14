
using Microsoft.EntityFrameworkCore;
using PastryShop.Application.Products.Queries;

namespace PastryShop.Application.Products.QueryHandlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, OperationResult<List<Product>>>
    {
        private readonly DataContext _ctx;

        public GetAllProductsQueryHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<List<Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<Product>>();

            try
            {
                var products = await _ctx.Products.ToListAsync(cancellationToken);

                if (products.Count == 0)
                {
                    result.AddError(ErrorCode.NotFound, ProductErrorMessages.ProductListIsEmpty);
                    return result;
                }

                result.Payload = products;

            }
            catch(Exception ex)
            {
                result.AddUnknownError(ex.Message);
            }

            return result;
        }
    }
}
