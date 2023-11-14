
namespace PastryShop.Api.Contracts.Identity.Request
{
    public class UserLoginRequest
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
