using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectName.Infrastructure.Security.Authentication
{
    public interface IJwtSecurityTokenService
    {
        Task<JwtTokenValue> CreateTokenAsync(ClaimsPrincipal principal);

        Task<JwtTokenValue> RefreshTokenAsync(
            string accessToken,
            string refreshToken);
    }
}