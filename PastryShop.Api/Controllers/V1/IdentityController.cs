
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
        [Route(ApiRoutes.Identity.Registration)]
        [ValidateModel]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest newUser, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<RegisterIdentityCommand>(newUser);

            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            var mapped = _mapper.Map<IdentityUserProfileResponse>(result.Payload);

            return Ok(mapped);
        }

        [HttpPost]
        [Route(ApiRoutes.Identity.Login)]
        [ValidateModel]
        public async Task<IActionResult> Login(UserLoginRequest login, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<LoginCommand>(login);

            var result = await _mediator.Send(command, cancellationToken);
            if (result.IsError) return HandleErrorResponse(result.Errors);

            var mapped = _mapper.Map<IdentityUserProfileResponse>(result.Payload);

            return Ok(mapped);
        }
    }
}
