using Polly;
using Polly.Retry;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;

namespace DotNetCore.Services;

public abstract class HttpService : IHttpService
{
    private readonly HttpClient _client;

    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

    protected HttpService(HttpOptions options)
    {
        _retryPolicy = Policy.Handle<HttpRequestException>().OrResult<HttpResponseMessage>(response => !response.IsSuccessStatusCode).WaitAndRetryAsync(options.RetryCount, _ => TimeSpan.FromSeconds(options.RetrySeconds));

        _client = new HttpClient { Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds) };

        _client.DefaultRequestHeaders.Clear();

        _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(nameof(HttpService), string.Empty));

        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

        if (options.BaseAddress is not null) _client.BaseAddress = new Uri(options.BaseAddress);

        if (options.Authentication is not null) _client.DefaultRequestHeaders.Authorization = options.Authentication;
    }

    public async Task<HttpStatusCode> DeleteAsync(string uri)
    {
        return (await _retryPolicy.ExecuteAsync(() => _client.DeleteAsync(uri))).StatusCode;
    }

    public async Task<Tuple<HttpStatusCode, TResponse>> GetAsync<TResponse>(string uri)
    {
        var response = await _retryPolicy.ExecuteAsync(() => _client.GetAsync(uri));

        return new(response.StatusCode, await response.Content.ReadFromJsonAsync<TResponse>());
    }

    public async Task<HttpStatusCode> PatchAsync(string uri, object value)
    {
        return (await _retryPolicy.ExecuteAsync(() => _client.PatchAsJsonAsync(uri, value))).StatusCode;
    }

    public async Task<HttpStatusCode> PostAsync(string uri, object value)
    {
        return (await _retryPolicy.ExecuteAsync(() => _client.PostAsJsonAsync(uri, value))).StatusCode;
    }

    public async Task<Tuple<HttpStatusCode, TResponse>> PostAsync<TResponse>(string uri, object value)
    {
        var response = await _retryPolicy.ExecuteAsync(() => _client.PostAsJsonAsync(uri, value));

        return new(response.StatusCode, await response.Content.ReadFromJsonAsync<TResponse>());
    }

    public async Task<HttpStatusCode> PutAsync(string uri, object value)
    {
        return (await _retryPolicy.ExecuteAsync(() => _client.PutAsJsonAsync(uri, value))).StatusCode;
    }
}
