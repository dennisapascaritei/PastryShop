
namespace PastryShop.Application.UserProfiles.QueryHandlers
{
    public class GetUserProfileByIdQueryHandler : IRequestHandler<GetUserProfileByIdQuery, OperationResult<UserProfile>>
    {
        private readonly DataContext _ctx;

        public GetUserProfileByIdQueryHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<UserProfile>> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserProfile>();

            try
            {
                var userProfile = await _ctx.UserProfiles
                    .Include(up => up.BasicInfo.ShippingAddress)
                    .FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId, cancellationToken);
                
                if (userProfile is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(UserProfileErrorMessages.UserProfileNotFound, request.UserProfileId));
                    return result;
                }

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
