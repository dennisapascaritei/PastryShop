using PastryShop.Api.Contracts.Identity.Request;
using PastryShop.Api.Contracts.Identity.Response;
using PastryShop.Application.Identity.Commands;
using PastryShop.Application.Identity.Dtos;

namespace PastryShop.Api.Mapping
{
    public class IdentityMappings : Profile
    {
        public IdentityMappings()
        {
            CreateMap<UserRegistrationRequest, RegisterIdentityCommand>();
            CreateMap<UserLoginRequest, LoginCommand>();
            CreateMap<IdentityUserProfileDto, IdentityUserProfileResponse>();
        }
    }
}
