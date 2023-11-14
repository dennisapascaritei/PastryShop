
namespace PastryShop.Api.Mapping
{
    public class UserProfileMapping : Profile
    {
        public UserProfileMapping()
        {
            CreateMap<UserProfile, UserProfileResponse>();
            CreateMap<BasicInfo, BasicInfoResponse>();
            CreateMap<ShippingAddress, ShippingAddressResponse>();
        }
    }
}
