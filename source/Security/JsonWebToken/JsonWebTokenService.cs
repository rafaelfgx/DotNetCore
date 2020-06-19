using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNetCore.Security
{
    public class JsonWebTokenService : IJsonWebTokenService
    {
        public JsonWebTokenService(JsonWebTokenSettings jsonWebTokenSettings)
        {
            JsonWebTokenSettings = jsonWebTokenSettings;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JsonWebTokenSettings.Key));

            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
        }

        private JsonWebTokenSettings JsonWebTokenSettings { get; }

        private SigningCredentials SigningCredentials { get; }

        public Dictionary<string, object> Decode(string token)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token).Payload;
        }

        public string Encode(IList<Claim> claims)
        {
            claims ??= new List<Claim>();

            var jwtSecurityToken = new JwtSecurityToken
            (
                JsonWebTokenSettings.Issuer,
                JsonWebTokenSettings.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.Add(JsonWebTokenSettings.Expires),
                SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
