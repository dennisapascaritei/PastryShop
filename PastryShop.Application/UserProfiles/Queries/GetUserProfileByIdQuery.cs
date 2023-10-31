
namespace PastryShop.Application.UserProfiles.Queries
{
    public class GetUserProfileByIdQuery : IRequest<OperationResult<UserProfile>>
    {
        public Guid UserProfileId { get; set; }
    }
}
