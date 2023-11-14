
namespace PastryShop.Domain.Aggregates.UserProfileAggregate
{
    
    public class UserProfile
    {
        private UserProfile() { }

        public Guid UserProfileId { get; private set; }
        public string IndentityId { get; private set; }
        public BasicInfo BasicInfo { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime DateUpdated { get; private set;}

        public static UserProfile CreateUserProfile(BasicInfo basicInfo, string identityId)
        {
            //To Do: add validation, error handling strategies, error notification strategies
            return new UserProfile
            {
                BasicInfo = basicInfo,
                IndentityId = identityId,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
            };
        }

        public void UpdateUserProfileBasicInfo(BasicInfo newBasicInfo)
        {
            BasicInfo = newBasicInfo;
            DateUpdated = DateTime.UtcNow;
        }
    }
}
