using System.Net.Http.Headers;

namespace DotNetCore.Services;

public sealed record HttpOptions
{
    public string BaseAddress { get; set; }

    public AuthenticationHeaderValue Authentication { get; set; }

    public int TimeoutSeconds { get; set; } = 5;

    public int RetryCount { get; set; }

    public int RetrySeconds { get; set; }
}
