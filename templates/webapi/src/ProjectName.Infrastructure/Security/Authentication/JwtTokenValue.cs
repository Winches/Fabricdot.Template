using Ardalis.GuardClauses;

namespace ProjectName.Infrastructure.Security.Authentication;

public class JwtTokenValue(
    string jti,
    string accessToken,
    string refreshToken,
    long expireIn,
    string tokenType = "Bearer")
{
    public string Jti { get; } = Guard.Against.NullOrEmpty(jti, nameof(jti));

    public string AccessToken { get; } = Guard.Against.NullOrEmpty(accessToken, nameof(accessToken));

    public string RefreshToken { get; } = Guard.Against.NullOrEmpty(refreshToken, nameof(refreshToken));

    public long ExpireIn { get; } = expireIn;

    public string TokenType { get; } = Guard.Against.NullOrEmpty(tokenType, nameof(tokenType));
}
