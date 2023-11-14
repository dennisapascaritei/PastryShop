
using PastryShop.Application.Identity.Dtos;

namespace PastryShop.Application.Identity.MappingProfiles
{
    public class IdentityMappings : Profile
    {
        public IdentityMappings()
        {
            CreateMap<UserProfile, IdentityUserProfileDto>()
                .ForMember(dest => dest.FirstName, opt
                => opt.MapFrom(src => src.BasicInfo.FirstName))
                .ForMember(dest => dest.LastName, opt
                => opt.MapFrom(src => src.BasicInfo.LastName))
                .ForMember(dest => dest.EmailAddress, opt
                => opt.MapFrom(src => src.BasicInfo.EmailAddress))
                .ForMember(dest => dest.Phone, opt
                => opt.MapFrom(src => src.BasicInfo.Phone))
                .ForMember(dest => dest.County, opt
                => opt.MapFrom(src => src.BasicInfo.ShippingAddress.County))
                .ForMember(dest => dest.City, opt
                => opt.MapFrom(src => src.BasicInfo.ShippingAddress.City))
                .ForMember(dest => dest.Address, opt
                => opt.MapFrom(src => src.BasicInfo.ShippingAddress.Address))
                .ForMember(dest => dest.PostCode, opt
                => opt.MapFrom(src => src.BasicInfo.ShippingAddress.PostCode));

        }
    }
}
