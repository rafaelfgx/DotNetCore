using System.Security.Claims;

namespace DotNetCore.Extensions;

public static class ClaimsPrincipalExtensions
{
    extension(ClaimsPrincipal claimsPrincipal)
    {
        public Claim Claim(string claimType) => claimsPrincipal?.FindFirst(claimType);

        public IEnumerable<string> ClaimRoles() => claimsPrincipal?.Claims("role");

        public IEnumerable<string> Claims(string claimType) => claimsPrincipal?.FindAll(claimType).Select(x => x.Value).ToList();

        public string ClaimSub() => claimsPrincipal?.Claim("sub")?.Value;

        public long Id() => long.TryParse(claimsPrincipal.ClaimSub(), out var value) ? value : 0;

        public IEnumerable<T> Roles<T>() where T : Enum => claimsPrincipal.ClaimRoles().Select(value => (T)Enum.Parse(typeof(T), value)).ToList();

        public T RolesFlag<T>() where T : Enum => (T)Enum.Parse(typeof(T), claimsPrincipal.Roles<T>().Sum(value => Convert.ToInt64(value)).ToString(), true);
    }
}
