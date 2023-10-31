using Microsoft.AspNetCore.Mvc;
using PastryShop.Api.Contracts.Identity.Request;

namespace PastryShop.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    public class IdentityController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public IdentityController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest newUser, CancellationToken cancellationToken)
        {
            var command = new 
        }
    }
}
