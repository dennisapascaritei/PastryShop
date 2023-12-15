
using PastryShop.Application.Identity.Dtos;

namespace PastryShop.Application.Identity.Commands
{
    public class RegisterIdentityCommand : IRequest<OperationResult<IdentityUserProfileDto>>
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
    }
}
