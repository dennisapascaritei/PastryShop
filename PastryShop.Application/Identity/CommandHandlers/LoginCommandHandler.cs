
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PastryShop.Application.Identity.Commands;
using PastryShop.Application.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace PastryShop.Application.Identity.CommandHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult<IdentityUserProfileDto>>
    {
        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityService _identityService;
        private OperationResult<IdentityUserProfileDto> _result = new();
        private readonly IMapper _mapper;

        public LoginCommandHandler(DataContext ctx, UserManager<IdentityUser> userManager, IdentityService identityService, IMapper mapper)
        {
            _ctx = ctx;
            _userManager = userManager;
            _identityService = identityService;
            _mapper = mapper;
        }
        public async Task<OperationResult<IdentityUserProfileDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var identityUser = await ValidateAndGetIdentityAsync(request);
                if (_result.IsError) return _result;

                var userProfile = await _ctx.UserProfiles
                    .Include(up => up.BasicInfo.ShippingAddress)
                    .FirstOrDefaultAsync(up => up.IndentityId == identityUser.Id);

                _result.Payload = _mapper.Map<IdentityUserProfileDto>(userProfile);
                _result.Payload.Username = identityUser.UserName;
                _result.Payload.Token = GetJwtToken(identityUser, userProfile);

            }
            catch (Exception e)
            {
                _result.AddUnknownError(e.Message);
            }

            return _result;
        }

        private string GetJwtToken(IdentityUser identityUser, UserProfile? userProfile)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
                    new Claim("IdentityId", identityUser.Id),
                    new Claim("UserProfileId", userProfile.UserProfileId.ToString())
                });

            var token = _identityService.CreateSecurityToken(claimsIdentity);

            return _identityService.WriteToken(token);
        }

        private async Task<IdentityUser> ValidateAndGetIdentityAsync(LoginCommand request)
        {
            var identityUser = await _userManager.FindByEmailAsync(request.Username);

            if (identityUser == null)
                _result.AddError(ErrorCode.IdentityUserDoesNotExist, IdentityErrorMessage.NonExistentIdentityUser);

            var validPassword = await _userManager.CheckPasswordAsync(identityUser, request.Password);

            if (!validPassword)
                _result.AddError(ErrorCode.IncorrectPassword, IdentityErrorMessage.IncorrectPassword);

            return identityUser;
        }
    }
}
