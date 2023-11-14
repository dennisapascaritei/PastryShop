
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using PastryShop.Application.Identity.Commands;
using PastryShop.Application.Identity.Dtos;
using PastryShop.Application.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PastryShop.Application.Identity.CommandHandlers
{
    public class RegisterIdentityCommandHandler : IRequestHandler<RegisterIdentityCommand, OperationResult<IdentityUserProfileDto>>
    {
        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityService _identityService;
        private OperationResult<IdentityUserProfileDto> _result = new();
        private readonly IMapper _mapper;

        public RegisterIdentityCommandHandler(DataContext ctx, UserManager<IdentityUser> userManager,
            IdentityService identityService, IMapper mapper)
        {
            _ctx = ctx;
            _userManager = userManager;
            _identityService = identityService;
            _mapper = mapper;
        }
        public async Task<OperationResult<IdentityUserProfileDto>> Handle(RegisterIdentityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await ValidateIdenityAlreadyExists(request, cancellationToken);
                if (_result.IsError) return _result;

                await using var transaction = await _ctx.Database.BeginTransactionAsync(cancellationToken);

                var identity = await CreateIdentityUserAsync(request, transaction, cancellationToken);
                if (_result.IsError) return _result;

                var profile = await CreateUserProfileAsync(request, identity, transaction, cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                _result.Payload = _mapper.Map<IdentityUserProfileDto>(profile);
                _result.Payload.Username = identity.UserName;
                _result.Payload.Token = GetJwtToken(identity, profile);
            }
            catch (Exception e)
            {
                _result.AddUnknownError(e.Message);
            }

            return _result;
        }

        private async Task ValidateIdenityAlreadyExists(RegisterIdentityCommand request, CancellationToken cancellationToken)
        {
            var identity = await _userManager.FindByEmailAsync(request.EmailAddress);

            if (identity != null)
                _result.AddError(ErrorCode.IdentityUserAlreadyExists, IdentityErrorMessage.IdentityUserAlreadyExists);
        }

        private async Task<IdentityUser> CreateIdentityUserAsync(RegisterIdentityCommand request, IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            var identity = new IdentityUser { Email = request.EmailAddress, UserName = request.EmailAddress };
            var createdIdentity = await _userManager.CreateAsync(identity, request.Password);

            if (!createdIdentity.Succeeded)
            {
                await transaction.RollbackAsync(cancellationToken);

                foreach (var identityError in createdIdentity.Errors)
                {
                    _result.AddError(ErrorCode.IdentityCreationFailed, identityError.Description);
                }
            }

            return identity;
        }

        private async Task<UserProfile> CreateUserProfileAsync(RegisterIdentityCommand request, IdentityUser identity, IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            try
            {
                var shippingAddress = ShippingAddress.CreateShippingAddress(request.County, request.City, request.Address, request.PostCode);
                var basicInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, request.EmailAddress, request.Phone, shippingAddress);

                var userProfile = UserProfile.CreateUserProfile(basicInfo, identity.Id);

                _ctx.UserProfiles.Add(userProfile);
                await _ctx.SaveChangesAsync(cancellationToken);

                return userProfile;
            }
            catch
            {
                transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
        
        private string GetJwtToken(IdentityUser identity, UserProfile profile)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, identity.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, identity.Email),
                        new Claim("IdentityId", identity.Id),
                        new Claim("UserProfileId", profile.UserProfileId.ToString())
                    });

            var token = _identityService.CreateSecurityToken(claimsIdentity);

            return _identityService.WriteToken(token);
        }
    }
}
