using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DotNetCore.Security;

public class JwtSettings
{
    public JwtSettings(string key, TimeSpan expires)
    {
        SecurityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(key));
        Expires = expires;
    }

    public JwtSettings(string key, TimeSpan expires, string audience, string issuer) : this(key, expires)
    {
        Audience = audience;
        Issuer = issuer;
    }

    public string Audience { get; }

    public TimeSpan Expires { get; }

    public string Issuer { get; }

    public SecurityKey SecurityKey { get; }

    public TokenValidationParameters TokenValidationParameters => new()
    {
        IssuerSigningKey = SecurityKey,
        ValidAudience = Audience,
        ValidIssuer = Issuer,
        ValidateAudience = !string.IsNullOrEmpty(Audience),
        ValidateIssuer = !string.IsNullOrEmpty(Issuer),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
}
