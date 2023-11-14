using PastryShop.Domain.Aggregates.UserProfileAggregate;

namespace PastryShop.Api.Contracts.UserProfiles.Response
{
    public class UserProfileResponse
    {
        public Guid UserProfileId { get; set; }
        public string IndentityId { get; set; }
        public BasicInfoResponse BasicInfo { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
