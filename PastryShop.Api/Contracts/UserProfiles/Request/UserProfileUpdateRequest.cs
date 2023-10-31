using PastryShop.Domain.Aggregates.UserProfileAggregate;
using System.ComponentModel.DataAnnotations;

namespace PastryShop.Api.Contracts.UserProfiles.Request
{
    public class UserProfileUpdateRequest
    {
        [Required]
        [MinLength(1)]
        [MaxLength(40)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(40)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

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
