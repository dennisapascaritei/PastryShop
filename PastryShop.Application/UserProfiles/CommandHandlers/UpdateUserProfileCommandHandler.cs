
using Microsoft.EntityFrameworkCore;
using PastryShop.Application.UserProfiles.Commands;

namespace PastryShop.Application.UserProfiles.CommandHandlers
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, OperationResult<UserProfile>>
    {
        private readonly DataContext _ctx;

        public UpdateUserProfileCommandHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<UserProfile>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserProfile>();

            try
            {
                var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId, cancellationToken);

                if (userProfile is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(UserProfileErrorMessages.UserProfileNotFound, request.UserProfileId));
                    return result;
                }

                var shippingAddress = ShippingAddress.CreateShippingAddress(request.County, request.City, request.Address, request.PostCode);
                var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, request.EmailAddress, request.Phone, shippingAddress);

                userProfile.UpdateUserProfileBasicInfo(basicInfo);

                _ctx.UserProfiles.Update(userProfile);
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
