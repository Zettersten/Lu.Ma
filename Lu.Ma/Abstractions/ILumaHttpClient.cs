using Lu.Ma.Http;

namespace Lu.Ma.Abstractions;

public interface ILumaHttpClient
{
    /// <summary>
    /// Sends a DELETE request to the specified URI.
    /// </summary>
    /// <param name="uri">The URI to send the request to.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the HTTP response message.</returns>
    Task<HttpResponseMessage> DeleteAsync(string uri, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a GET request to the specified URI.
    /// </summary>
    /// <param name="uri">The URI to send the request to.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the HTTP response message.</returns>
    Task<HttpResponseMessage> GetAsync(string uri, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a POST request to the specified URI.
    /// </summary>
    /// <param name="uri">The URI to send the request to.</param>
    /// <param name="content">The HTTP request content sent to the server.</param>
    /// <param name="cancellationToken">The cancellation token.</param>-
    /// <returns>A task that represents the asynchronous operation. The task result contains the HTTP response message.</returns>
    Task<HttpResponseMessage> PostAsync(string uri, HttpContent content, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a PUT request to the specified URI.
    /// </summary>
    /// <param name="uri">The URI to send the request to.</param>
    /// <param name="content">The HTTP request content sent to the server.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the HTTP response message.</returns>
    Task<HttpResponseMessage> PutAsync(string uri, HttpContent content, CancellationToken cancellationToken = default);

    /// <summary>
    /// Underlying function to send a request to the API and deserialize the response.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="requestFunc">Function to invoke</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task with response object</returns>
    /// <exception cref="LumaApiException"></exception>
    Task<T> SendRequestAsync<T>(Func<LumaHttpClient, CancellationToken, Task<HttpResponseMessage>> requestFunc, CancellationToken cancellationToken = default);
}