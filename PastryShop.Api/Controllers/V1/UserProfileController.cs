
namespace PastryShop.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserProfileController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UserProfileController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserProfiles(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllUserProfilesQuery(), cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);
            var mapped = _mapper.Map<List<UserProfileResponse>>(result.Payload);

            return Ok(mapped);
        }

        [HttpGet]
        [Route(ApiRoutes.UserProfiles.UserProfileId)]
        [ValidateGuid("userProfileId")]
        public async Task<IActionResult> GetUserProfileById(string userProfileId, CancellationToken cancellationToken)
        {
            var query = new GetUserProfileByIdQuery
            {
                UserProfileId = Guid.Parse(userProfileId)
            };

            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            var mapped = _mapper.Map<UserProfileResponse>(result.Payload);

            return Ok(mapped);
        }        

        [HttpPut]
        [Route(ApiRoutes.UserProfiles.UserProfileId)]
        [ValidateGuid("userProfileId")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileUpdateRequest updatedUserProfile, string userProfileId, CancellationToken cancellationToken)
        {
            var command = new UpdateUserProfileCommand
            {
                UserProfileId = Guid.Parse(userProfileId),
                FirstName = updatedUserProfile.FirstName,
                LastName = updatedUserProfile.LastName,
                EmailAddress = updatedUserProfile.EmailAddress,
                Phone = updatedUserProfile.Phone,
                County = updatedUserProfile.County,
                City = updatedUserProfile.City,
                Address = updatedUserProfile.Address,
                PostCode = updatedUserProfile.PostCode
            };

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }

        [HttpDelete]
        [Route(ApiRoutes.UserProfiles.UserProfileId)]
        [ValidateGuid("userProfileId")]
        public async Task<IActionResult> DeleteUserProfile(string userProfileId, CancellationToken cancellationToken)
        {
            var command = new DeleteUserProfileCommand { UserProfileId = Guid.Parse(userProfileId) };

            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            return NoContent();
        }
    }
}
