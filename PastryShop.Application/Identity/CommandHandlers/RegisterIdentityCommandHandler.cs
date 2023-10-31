
using Microsoft.AspNetCore.Identity;
using PastryShop.Application.Identity.Commands;
using PastryShop.Application.Identity.Dtos;
using PastryShop.Application.Services;

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

                var 
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task ValidateIdenityAlreadyExists(RegisterIdentityCommand request, CancellationToken cancellationToken)
        {
            var identity = await _userManager.FindByEmailAsync(request.EmailAddress);

            if (identity != null)
                _result.AddError(ErrorCode.IdentityUserAlreadyExists, IdentityErrorMessage.IdentityUserAlreadyExists);
        }
    }
}
