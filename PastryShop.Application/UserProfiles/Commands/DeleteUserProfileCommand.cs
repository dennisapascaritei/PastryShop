
namespace PastryShop.Application.UserProfiles.Commands
{
    public class DeleteUserProfileCommand : IRequest<OperationResult<UserProfile>>
    {
        public Guid UserProfileId { get; set; }
    }
}
