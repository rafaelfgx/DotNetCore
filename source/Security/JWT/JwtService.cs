using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DotNetCore.Security;

public class JwtService : IJwtService
{
    public JwtService(JwtSettings settings)
    {
        Settings = settings;
    }

    private JwtSettings Settings { get; }

    public Dictionary<string, object> Decode(string token)
    {
        return new JwtSecurityTokenHandler().ReadJwtToken(token).Payload;
    }

    public string Encode(IList<Claim> claims)
    {
        var token = new JwtSecurityToken
        (
            Settings.Issuer,
            Settings.Audience,
            claims ?? new List<Claim>(),
            DateTime.UtcNow,
            DateTime.UtcNow.Add(Settings.Expires),
            new SigningCredentials(Settings.SecurityKey, SecurityAlgorithms.HmacSha512)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
