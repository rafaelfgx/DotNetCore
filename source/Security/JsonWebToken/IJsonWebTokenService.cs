using System.Collections.Generic;
using System.Security.Claims;

namespace DotNetCore.Security
{
    public interface IJsonWebTokenService
    {
        Dictionary<string, object> Decode(string token);

        string Encode(IList<Claim> claims);
    }
}
