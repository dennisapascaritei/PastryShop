
using PastryShop.Application.UserProfiles.Commands;

namespace PastryShop.Application.UserProfiles.CommandHandlers
{
    public class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand, OperationResult<UserProfile>>
    {
        private readonly DataContext _ctx;
        public CreateUserProfileCommandHandler(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<OperationResult<UserProfile>> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserProfile>();
            try
            {
                var shippingAddress = ShippingAddress.CreateShippingAddress(request.County, request.City, request.Address, request.PostCode);
                var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, request.EmailAddress, request.Phone, shippingAddress);

                var userProfile = UserProfile.CreateUserProfile(basicInfo, request.IndentityId);

                _ctx.UserProfiles.Add(userProfile);
                await _ctx.SaveChangesAsync(cancellationToken);

                result.Payload = userProfile;
            }
            catch (Exception ex)
            {
                result.AddUnknownError(ex.Message);
            }

            return result;
        }
    }
}
