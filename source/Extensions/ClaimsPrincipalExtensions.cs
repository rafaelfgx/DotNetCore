using System.Security.Claims;

namespace DotNetCore.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Claim Claim(this ClaimsPrincipal claimsPrincipal, string claimType) => claimsPrincipal?.FindFirst(claimType);

    public static IEnumerable<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal) => claimsPrincipal?.Claims("role");

    public static IEnumerable<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType) => claimsPrincipal?.FindAll(claimType).Select(x => x.Value).ToList();

    public static string ClaimSub(this ClaimsPrincipal claimsPrincipal) => claimsPrincipal?.Claim("sub")?.Value;

    public static long Id(this ClaimsPrincipal claimsPrincipal) => long.TryParse(claimsPrincipal.ClaimSub(), out var value) ? value : 0;

    public static IEnumerable<T> Roles<T>(this ClaimsPrincipal claimsPrincipal) where T : Enum => claimsPrincipal.ClaimRoles().Select(value => (T)Enum.Parse(typeof(T), value)).ToList();

    public static T RolesFlag<T>(this ClaimsPrincipal claimsPrincipal) where T : Enum => (T)Enum.Parse(typeof(T), claimsPrincipal.Roles<T>().Sum(value => Convert.ToInt64(value)).ToString(), true);
}
