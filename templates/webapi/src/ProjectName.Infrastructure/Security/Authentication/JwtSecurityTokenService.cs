using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Fabricdot.Core.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ProjectName.Infrastructure.Security.Authentication;

public class JwtSecurityTokenService(
    IOptions<JwtSecurityTokenOptions> options,
    ILogger<JwtSecurityTokenService> logger) : IJwtSecurityTokenService, ISingletonDependency
{
    protected const string RefreshTokenAudience = "_refresh";
    protected JwtSecurityTokenOptions Options { get; } = options.Value;
    protected JwtSecurityTokenHandler TokenHandler { get; } = new();
    protected ILogger<JwtSecurityTokenService> Logger { get; } = logger;

    public virtual async Task<JwtTokenValue> CreateTokenAsync(ClaimsPrincipal principal)
    {
        var accessToken = await CreateAccessTokenAsync(principal.Identities.First());
        var refreshToken = await CreateRefreshTokenAsync(accessToken);

        return new JwtTokenValue(
            accessToken.Id,
            TokenHandler.WriteToken(accessToken),
            refreshToken,
            accessToken.ValidTo.ToTimestamp());
    }

    public virtual async Task<JwtTokenValue> RefreshTokenAsync(
        string accessToken,
        string refreshToken)
    {
        TokenHandler.EnsureReadable(accessToken, nameof(accessToken));
        TokenHandler.EnsureReadable(refreshToken, nameof(refreshToken));

        var validationParameters = GetTokenValidationParameters();
        var claimsPrincipal = TokenHandler.ValidateToken(
            accessToken,
            "access token",
            validationParameters,
            out var validatedAccessToken);
        if (!validatedAccessToken.IsExpired())
            throw new InvalidOperationException("Access token is not expired.");

        validationParameters.ValidateLifetime = true;
        validationParameters.ValidAudience = RefreshTokenAudience;
        _ = TokenHandler.ValidateToken(
            refreshToken,
            "refresh token",
            validationParameters,
            out var validateRefreshToken);

        // Make sure they belong to same user.
        if (!validatedAccessToken.HasSameClaim(validateRefreshToken, JwtRegisteredClaimNames.NameId))
            throw new InvalidOperationException("Invalid refresh token.");

        var newAccessToken = await CreateAccessTokenAsync(claimsPrincipal.Identities.First());
        return new JwtTokenValue(
            newAccessToken.Id,
            TokenHandler.WriteToken(newAccessToken),
            refreshToken,
            newAccessToken.ValidTo.ToTimestamp());
    }

    protected virtual Task<JwtSecurityToken> CreateAccessTokenAsync(ClaimsIdentity subject)
    {
        var jti = Guid.NewGuid().ToString();
        var signingCredentials = GetSigningCredentials(Options.SecretKey);

        if (!subject.HasClaim(v => v.Type == JwtRegisteredClaimNames.Jti))
        {
            subject.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, jti));
        }
        if (!subject.HasClaim(v => v.Type == JwtRegisteredClaimNames.Aud))
        {
            var audiences = Options.Audiences.Select(v => new Claim(JwtRegisteredClaimNames.Aud, v));
            subject.AddClaims(audiences);
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = subject,
            Expires = DateTime.UtcNow.AddMinutes(Options.AccessTokenExpireMinutes),
            Issuer = Options.Issuer,
            SigningCredentials = signingCredentials
        };

        var token = TokenHandler.CreateToken(tokenDescriptor);
        return Task.FromResult((JwtSecurityToken)token);
    }

    protected virtual Task<string> CreateRefreshTokenAsync(JwtSecurityToken accessToken)
    {
        var signingCredentials = GetSigningCredentials(Options.SecretKey);
        var claims = new List<Claim>
        {
            accessToken.Claims.Single(v=>v.Type==JwtRegisteredClaimNames.NameId),
            new(JwtRegisteredClaimNames.Aud,RefreshTokenAudience),
        };

        var refreshTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(Options.RefreshTokenExpireMinuts),
            Issuer = Options.Issuer,
            SigningCredentials = signingCredentials
        };
        var encodedToken = TokenHandler.CreateEncodedJwt(refreshTokenDescriptor);
        return Task.FromResult(encodedToken);
    }

    protected virtual TokenValidationParameters GetTokenValidationParameters()
    {
        var signingCredentials = GetSigningCredentials(Options.SecretKey);
        return new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = Options.Issuer,
            ValidAudiences = Options.Audiences,
            IssuerSigningKey = signingCredentials.Key
        };
    }

    private static SigningCredentials GetSigningCredentials(string secretKey)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
        return new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
    }
}
