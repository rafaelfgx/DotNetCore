using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DotNetCore.Security;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration) => _configuration = configuration;

    public Dictionary<string, object> Decode(string token) => new JwtSecurityTokenHandler().ReadJwtToken(token).Payload;

    public string Encode(IList<Claim> claims)
    {
        var configuration = _configuration.GetSection("Authentication").GetChildren().First().GetChildren().First();

        var key = configuration.GetSection("SigningKeys").GetChildren().First()["Value"];

        var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(key ?? throw new ArgumentNullException()));

        var token = new JwtSecurityToken
        (
            configuration[nameof(TokenValidationParameters.ValidIssuer)],
            configuration[nameof(TokenValidationParameters.ValidAudience)],
            claims ?? new List<Claim>(),
            DateTime.UtcNow,
            DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
            new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string Encode(string sub, string[] roles)
    {
        var claims = new List<Claim> { new("sub", sub) };

        roles.ToList().ForEach(role => claims.Add(new Claim("role", role)));

        return Encode(claims);
    }
}
