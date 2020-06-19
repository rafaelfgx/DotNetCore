using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace DotNetCore.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Claim Claim(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            return claimsPrincipal?.FindFirst(claimType);
        }

        public static IEnumerable<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims("role");
        }

        public static IEnumerable<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            return claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
        }

        public static string ClaimSub(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claim("sub")?.Value;
        }

        public static long Id(this ClaimsPrincipal claimsPrincipal)
        {
            return long.TryParse(claimsPrincipal.ClaimSub(), out var value) ? value : 0;
        }

        public static IEnumerable<T> Roles<T>(this ClaimsPrincipal claimsPrincipal) where T : Enum
        {
            return claimsPrincipal.ClaimRoles().Select(value => (T)Enum.Parse(typeof(T), value)).ToList();
        }

        public static T RolesFlag<T>(this ClaimsPrincipal claimsPrincipal) where T : Enum
        {
            var roles = claimsPrincipal.Roles<T>().Sum(value => Convert.ToInt64(value));

            return (T)Enum.Parse(typeof(T), roles.ToString(), true);
        }
    }
}
