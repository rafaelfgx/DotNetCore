using System;

namespace DotNetCore.Security
{
    public class JsonWebTokenSettings
    {
        public JsonWebTokenSettings
        (
            string key,
            TimeSpan expires
        )
        {
            Key = key;
            Expires = expires;
        }

        public JsonWebTokenSettings
        (
            string key,
            TimeSpan expires,
            string audience,
            string issuer
        )
        : this(key, expires)
        {
            Audience = audience;
            Issuer = issuer;
        }

        public string Audience { get; }

        public TimeSpan Expires { get; }

        public string Issuer { get; }

        public string Key { get; }
    }
}
