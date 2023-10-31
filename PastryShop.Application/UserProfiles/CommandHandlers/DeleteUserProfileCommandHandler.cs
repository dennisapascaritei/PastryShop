
using Microsoft.EntityFrameworkCore;
using PastryShop.Application.UserProfiles.Commands;

namespace PastryShop.Application.UserProfiles.CommandHandlers
{
    public class DeleteUserProfileCommandHandler : IRequestHandler<DeleteUserProfileCommand, OperationResult<UserProfile>>
    {
        private readonly DataContext _ctx;

        public DeleteUserProfileCommandHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<UserProfile>> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserProfile>();

            try
            {
                var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(ur => ur.UserProfileId == request.UserProfileId, cancellationToken);

                if (userProfile is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(UserProfileErrorMessages.UserProfileNotFound, request.UserProfileId));
                    return result;
                }

                _ctx.UserProfiles.Remove(userProfile);
                await _ctx.SaveChangesAsync(cancellationToken);

                result.Payload = userProfile;
            }

            catch(Exception ex)
            {
                result.AddUnknownError(ex.Message);
            }

            return result;

        }
    }
}
