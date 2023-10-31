using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PastryShop.Dal;

namespace PastryShop.Api.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductController : Controller
    {
        private readonly DataContext _ctx;

        public ProductController(DataContext ctx)
        {
            _ctx = ctx;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _ctx.Products.ToListAsync();

            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductById(string productId)
        {
            var productGuid = Guid.Parse(productId);
            var product = await _ctx.Products.FirstOrDefaultAsync(p => p.ProductId == productGuid);

            return Ok(product);
        }
    }
}
