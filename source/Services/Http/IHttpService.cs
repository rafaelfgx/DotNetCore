using System.Net;

namespace DotNetCore.Services;

public interface IHttpService
{
    Task<HttpStatusCode> DeleteAsync(string uri);

    Task<(HttpStatusCode, TResponse)> GetAsync<TResponse>(string uri);

    Task<HttpStatusCode> PatchAsync(string uri, object value);

    Task<HttpStatusCode> PostAsync(string uri, object value);

    Task<(HttpStatusCode, TResponse)> PostAsync<TResponse>(string uri, object value);

    Task<HttpStatusCode> PutAsync(string uri, object value);
}
