using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace ProjectName.Infrastructure.Security.Authentication;

public static class JwtSecurityTokenHandlerExtensions
{
    public static void EnsureReadable(
        this JwtSecurityTokenHandler tokenHandler,
        string token,
        string name)
    {
        if (!tokenHandler.CanReadToken(token))
            throw new InvalidOperationException($"Unreadable security token :{name}.");
    }

    public static ClaimsPrincipal ValidateToken(
        this JwtSecurityTokenHandler tokenHandler,
        string token,
        string name,
        TokenValidationParameters validationParameters,
        out JwtSecurityToken validatedToken)
    {
        try
        {
            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var securityToken);
            validatedToken = (JwtSecurityToken)securityToken;
            return claimsPrincipal;
        }
        catch (SecurityTokenException ex)
        {
            throw new InvalidOperationException($"Invalid security token :{name}.", ex);
        }
    }
}
