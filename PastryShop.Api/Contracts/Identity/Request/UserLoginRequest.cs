
namespace PastryShop.Api.Contracts.Identity.Request
{
    public class UserLoginRequest
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
