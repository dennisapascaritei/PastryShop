
using System.ComponentModel.DataAnnotations;

namespace PastryShop.Application.UserProfiles.Commands
{
    public class CreateUserProfileCommand : IRequest<OperationResult<UserProfile>>
    {
        public Guid UserProfileId { get; set; }
        public string IndentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
    }
}
