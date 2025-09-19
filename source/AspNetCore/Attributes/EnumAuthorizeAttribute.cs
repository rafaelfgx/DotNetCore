using Microsoft.AspNetCore.Authorization;

namespace DotNetCore.AspNetCore;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class EnumAuthorizeAttribute : AuthorizeAttribute
{
    public EnumAuthorizeAttribute(params object[] roles) => Roles = string.Join(",", roles.Select(role => Enum.GetName(role.GetType(), role)));
}
