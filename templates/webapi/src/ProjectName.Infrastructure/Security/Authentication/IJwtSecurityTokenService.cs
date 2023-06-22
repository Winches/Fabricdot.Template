using System.Security.Claims;

namespace ProjectName.Infrastructure.Security.Authentication;

public interface IJwtSecurityTokenService
{
    Task<JwtTokenValue> CreateTokenAsync(ClaimsPrincipal principal);

    Task<JwtTokenValue> RefreshTokenAsync(
        string accessToken,
        string refreshToken);
}
