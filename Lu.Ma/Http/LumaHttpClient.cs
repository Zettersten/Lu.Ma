using Lu.Ma.Extensions;
using Lu.Ma.Models.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using System.Text.Json;

namespace Lu.Ma.Http;

/// <summary>
/// A resilient HTTP client for making requests to the Luma API.
/// </summary>
public sealed class LumaHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<LumaHttpClient> _logger;
    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

    /// <summary>
    /// Initializes a new instance of the <see cref="LumaHttpClient"/> class.
    /// </summary>
    /// <param name="httpClient">The HttpClient instance to use.</param>
    /// <param name="logger">The logger instance.</param>
    /// <param name="options">The options instance.</param>
    public LumaHttpClient(HttpClient httpClient, ILogger<LumaHttpClient> logger, IOptions<LumaApiOptions> options)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        var apiKey = options.Value.ApiKey;

        if (string.IsNullOrEmpty(apiKey))
        {
            throw new ArgumentException("Luma API key is not configured.", nameof(options));
        }

        // Configure the HttpClient with the API key
        _httpClient.DefaultRequestHeaders.Add("x-luma-api-key", apiKey);

        // Retry policy configuration (as previously defined)
        _retryPolicy = Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: (retryAttempt, response, context) =>
                {
                    if (response.Result?.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                    {
                        return TimeSpan.FromMinutes(1);
                    }
                    return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
                },
                onRetryAsync: (outcome, timespan, retryAttempt, context) =>
                {
                    if (outcome.Result?.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                    {
                        _logger.LogWarning("Rate limit hit. Waiting for 1 minute before retry {retry}.", retryAttempt);
                    }
                    else
                    {
                        _logger.LogWarning("Delaying for {delay}ms, then making retry {retry}.", timespan.TotalMilliseconds, retryAttempt);
                    }
                    return Task.CompletedTask;
                }
            );
    }

    /// <summary>
    /// Sends a GET request to the specified URI.
    /// </summary>
    /// <param name="uri">The URI to send the request to.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the HTTP response message.</returns>
    public async Task<HttpResponseMessage> GetAsync(string uri, CancellationToken cancellationToken = default)
    {
        return await _retryPolicy.ExecuteAsync((c) => _httpClient.GetAsync(uri, c), cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Sends a POST request to the specified URI.
    /// </summary>
    /// <param name="uri">The URI to send the request to.</param>
    /// <param name="content">The HTTP request content sent to the server.</param>
    /// <param name="cancellationToken">The cancellation token.</param>-
    /// <returns>A task that represents the asynchronous operation. The task result contains the HTTP response message.</returns>
    public async Task<HttpResponseMessage> PostAsync(string uri, HttpContent content, CancellationToken cancellationToken = default)
    {
        return await _retryPolicy.ExecuteAsync((c) => _httpClient.PostAsync(uri, content, c), cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Sends a PUT request to the specified URI.
    /// </summary>
    /// <param name="uri">The URI to send the request to.</param>
    /// <param name="content">The HTTP request content sent to the server.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the HTTP response message.</returns>
    public async Task<HttpResponseMessage> PutAsync(string uri, HttpContent content, CancellationToken cancellationToken = default)
    {
        return await _retryPolicy.ExecuteAsync((c) => _httpClient.PutAsync(uri, content, c), cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Sends a DELETE request to the specified URI.
    /// </summary>
    /// <param name="uri">The URI to send the request to.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the HTTP response message.</returns>
    public async Task<HttpResponseMessage> DeleteAsync(string uri, CancellationToken cancellationToken = default)
    {
        return await _retryPolicy.ExecuteAsync((c) => _httpClient.DeleteAsync(uri, c), cancellationToken: cancellationToken);
    }

    internal async Task<T> SendRequestAsync<T>(Func<LumaHttpClient, CancellationToken, Task<HttpResponseMessage>> requestFunc, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await requestFunc(this, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsJsonAsync<T>(cancellationToken);

                return result is null
                    ? throw new LumaApiException(
                        message: $"API request succeeded but failed (returned null) on serialization.",
                        apiError: null
                    )
                    : result;
            }
            else
            {
                var error = await response.Content.ReadAsJsonAsync<Error>(cancellationToken);
                throw new LumaApiException(
                    $"API request failed with status code {response.StatusCode}",
                    error,
                    (int)response.StatusCode);
            }
        }
        catch (JsonException e)
        {
            throw new LumaApiException("Failed to deserialize API response", new Error
            {
                Message = e.Message,
                Code = e.GetHashCode()
            });
        }
        catch (HttpRequestException e)
        {
            int statusCode = e.StatusCode.HasValue ?
                (int)e.StatusCode.Value
                : 500;

            throw new LumaApiException("Failed to deserialize API response", new Error
            {
                Message = e.Message,
                Code = e.GetHashCode(),
            }, statusCode);
        }
    }
}