

namespace PastryShop.Application.Identity.Dtos
{
    public class IdentityUserProfileDto
    {
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; }
        public string Phone { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Token { get; set; }
    }
}
