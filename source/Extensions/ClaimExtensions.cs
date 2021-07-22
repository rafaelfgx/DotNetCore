using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace DotNetCore.Extensions
{
    public static class ClaimExtensions
    {
        public static void AddJti(this ICollection<Claim> claims)
        {
            claims?.Add(new Claim("jti", Guid.NewGuid().ToString()));
        }

        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {
            roles.ToList().ForEach(role => claims.Add(new Claim("role", role)));
        }

        public static void AddSub(this ICollection<Claim> claims, string sub)
        {
            claims?.Add(new Claim("sub", sub));
        }
    }
}
