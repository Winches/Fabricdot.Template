using System.IdentityModel.Tokens.Jwt;
using Ardalis.GuardClauses;

namespace ProjectName.Infrastructure.Security.Authentication;

public static class JwtSecurityTokenExtensions
{
    public static bool HasSameClaim(
        this JwtSecurityToken left,
        JwtSecurityToken right,
        string claimType)
    {
        Guard.Against.Null(left, nameof(left));
        Guard.Against.NullOrEmpty(claimType, nameof(claimType));

        var leftClaim = left.Claims.SingleOrDefault(v => v.Type == claimType);
        var rightClaim = right.Claims.SingleOrDefault(v => v.Type == claimType);

        return leftClaim?.Value?.Equals(rightClaim?.Value) ?? false;
    }

    public static bool IsExpired(this JwtSecurityToken token)
    {
        Guard.Against.Null(token, nameof(token));

        return token.ValidTo <= DateTime.UtcNow;
    }

    public static bool IsInAudience(this JwtSecurityToken token, string audience)
    {
        Guard.Against.Null(token, nameof(token));
        Guard.Against.NullOrEmpty(audience, nameof(audience));

        return token.Audiences.Contains(audience);
    }
}
