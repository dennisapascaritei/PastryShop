
using Microsoft.EntityFrameworkCore;

namespace PastryShop.Application.Products.CommandHandlers
{
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, OperationResult<Product>>
    {
        private readonly DataContext _ctx;
        private readonly IMapper _mapper;

        public ProductUpdateCommandHandler(DataContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<OperationResult<Product>> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Product>();
            try
            {
                var product = await _ctx.Products.FirstOrDefaultAsync(pr => pr.ProductId == request.ProductId, cancellationToken);
                if (product is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(ProductErrorMessages.ProductNotFound, request.ProductId));
                    return result;
                }
                var mapped = _mapper.Map<Product>(request);
                product.UpdateProduct(mapped);

                await _ctx.SaveChangesAsync(cancellationToken);

                result.Payload = mapped;
            }
            catch(Exception ex)
            {
                result.AddUnknownError(ex.Message);
            }

            return result;
        }
    }
}
