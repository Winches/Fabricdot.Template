using Ardalis.GuardClauses;

namespace ProjectName.Infrastructure.Security.Authentication;

public class JwtTokenValue
{
    public string Jti { get; }

    public string AccessToken { get; }

    public string RefreshToken { get; }

    public long ExpireIn { get; }

    public string TokenType { get; }

    public JwtTokenValue(
        string jti,
        string accessToken,
        string refreshToken,
        long expireIn,
        string tokenType = "Bearer")
    {
        Jti = Guard.Against.NullOrEmpty(jti, nameof(jti));
        AccessToken = Guard.Against.NullOrEmpty(accessToken, nameof(accessToken));
        RefreshToken = Guard.Against.NullOrEmpty(refreshToken, nameof(refreshToken));
        ExpireIn = expireIn;
        TokenType = Guard.Against.NullOrEmpty(tokenType, nameof(tokenType));
    }
}
