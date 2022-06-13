using System.Threading;
using System.Threading.Tasks;
using Fabricdot.Infrastructure.Commands;
using ProjectName.Infrastructure.Security.Authentication;

namespace ProjectName.WebApi.Application.Commands.Authentication
{
    internal class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, JwtTokenValue>
    {
        private readonly IJwtSecurityTokenService _jwtSecurityTokenService;

        public RefreshTokenCommandHandler(IJwtSecurityTokenService jwtSecurityTokenService)
        {
            _jwtSecurityTokenService = jwtSecurityTokenService;
        }

        public async Task<JwtTokenValue> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            return await _jwtSecurityTokenService.RefreshTokenAsync(
                request.AccessToken,
                request.RefreshToken);
        }
    }
}