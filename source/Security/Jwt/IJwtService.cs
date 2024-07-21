using System.Security.Claims;

namespace DotNetCore.Security;

public interface IJwtService
{
    Dictionary<string, object> Decode(string token);

    string Encode(IList<Claim> claims);

    string Encode(string sub, string[] roles);
}
