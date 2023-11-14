using System.ComponentModel;

namespace PastryShop.Api.Contracts.Identity.Request
{
    public class UserRegistrationRequest
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(40)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(40)]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(40)]
        public string County { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(40)]
        public string City { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(40)]
        public string Address { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(40)]
        public string PostCode { get; set; }

    }
}
