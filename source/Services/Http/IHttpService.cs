using System.Net;

namespace DotNetCore.Services;

public interface IHttpService
{
    Task<HttpStatusCode> DeleteAsync(string uri);

    Task<Tuple<HttpStatusCode, TResponse>> GetAsync<TResponse>(string uri);

    Task<HttpStatusCode> PatchAsync(string uri, object value);

    Task<HttpStatusCode> PostAsync(string uri, object value);

    Task<Tuple<HttpStatusCode, TResponse>> PostAsync<TResponse>(string uri, object value);

    Task<HttpStatusCode> PutAsync(string uri, object value);
}
