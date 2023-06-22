namespace ProjectName.Infrastructure.Security.Authentication;

public class JwtSecurityTokenOptions
{
    public const string Name = "Jwt";

    public string SecretKey { get; set; } = null!;

    public long AccessTokenExpireMinutes { get; set; } = 20;

    public long RefreshTokenExpireMinuts { get; set; } = 24 * 60 * 7;

    public string Issuer { get; set; } = null!;

    public string Audience { get; set; } = null!;

    public string[] Audiences => Audience.Split(";");
}
