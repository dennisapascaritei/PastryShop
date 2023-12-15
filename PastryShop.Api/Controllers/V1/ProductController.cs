
using Microsoft.AspNetCore.Authorization;
using PastryShop.Api.Filters;

namespace PastryShop.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route(ApiRoutes.BaseRoute)]
    public class ProductController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllProductsQuery(), cancellationToken);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            var mapped = _mapper.Map<List<ProductResponse>>(result.Payload);

            return Ok(mapped);
        }

        [HttpGet]
        [Route(ApiRoutes.Products.ProductId)]
        [ValidateGuid("productId")]
        public async Task<IActionResult> GetProductById(string productId, CancellationToken cancellationToken)
        {
            var productGuid = Guid.Parse(productId);
            var query = new GetProductByIdQuery { ProductId = productGuid };
            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            var mapped = _mapper.Map<ProductResponse>(result.Payload);

            return Ok(mapped);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateRequest newProduct, CancellationToken cancellationToken)
        {
            var command = new ProductCreateCommand
            {
                Name = newProduct.Name,
                Description = newProduct.Description,
                Price = newProduct.Price,
                Weight = newProduct.Weight,
                ImageURL = newProduct.ImageURL
            };

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            var mapped = _mapper.Map<ProductResponse>(result.Payload);

            return Ok(mapped);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route(ApiRoutes.Products.ProductId)]
        [ValidateGuid("productId")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateRequest updatedProduct, string productId, CancellationToken cancellationToken)
        {
            var command = new ProductUpdateCommand
            {
                ProductId = Guid.Parse(productId),
                Name = updatedProduct.Name,
                Description = updatedProduct.Description,
                Price = updatedProduct.Price,
                Weight = updatedProduct.Weight,
                ImageURL = updatedProduct.ImageURL
            };

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route(ApiRoutes.Products.ProductId)]
        [ValidateGuid("productId")]
        public async Task<IActionResult> DeleteProduct(string productId, CancellationToken cancellationToken)
        {
            var command = new ProductDeleteCommand { ProductId = Guid.Parse(productId) };
            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }
    }
}
