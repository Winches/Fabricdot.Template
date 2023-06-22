using Fabricdot.Infrastructure.Commands;
using ProjectName.Infrastructure.Security.Authentication;

namespace ProjectName.WebApi.Application.Commands.Authentication;

internal class RefreshTokenCommandHandler : CommandHandler<RefreshTokenCommand, JwtTokenValue>
{
    private readonly IJwtSecurityTokenService _jwtSecurityTokenService;

    public RefreshTokenCommandHandler(IJwtSecurityTokenService jwtSecurityTokenService)
    {
        _jwtSecurityTokenService = jwtSecurityTokenService;
    }

    public override async Task<JwtTokenValue> ExecuteAsync(
        RefreshTokenCommand command,
        CancellationToken cancellationToken)
    {
        return await _jwtSecurityTokenService.RefreshTokenAsync(
            command.AccessToken,
            command.RefreshToken);
    }
}
