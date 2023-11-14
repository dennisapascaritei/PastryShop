﻿
namespace PastryShop.Application.Identity.Commands
{
    public class LoginCommand : IRequest<OperationResult<IdentityUserProfileDto>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
