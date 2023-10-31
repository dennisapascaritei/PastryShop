
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PastryShop.Application.Identity.Dtos
{
    public class IdentityUserProfileDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Token { get; set; }
    }
}
