
using Microsoft.EntityFrameworkCore;
using PastryShop.Application.UserProfiles.Queries;

namespace PastryShop.Application.UserProfiles.QueryHandlers
{
    public class GetAllUserProfilesQueryHandler : IRequestHandler<GetAllUserProfilesQuery, OperationResult<List<UserProfile>>>
    {
        private readonly DataContext _ctx;

        public GetAllUserProfilesQueryHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<List<UserProfile>>> Handle(GetAllUserProfilesQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<UserProfile>>();
            try
            {
                var userProfiles = await _ctx.UserProfiles
                    .Include(up => up.BasicInfo.ShippingAddress)
                    .ToListAsync(cancellationToken);

                if (userProfiles is null)
                {
                    result.AddError(ErrorCode.NotFound, UserProfileErrorMessages.UserProfileListIsEmpty);
                    return result;
                }

                result.Payload = userProfiles;

            }
            catch (Exception ex)
            {
                result.AddUnknownError(ex.Message);
            }

            return result;
        }
    }
}
