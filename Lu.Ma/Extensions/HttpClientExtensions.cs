using Lu.Ma.Http;

namespace Lu.Ma.Extensions;

using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Provides extension methods for HttpClient to simplify JSON-based API interactions.
/// </summary>
public static class HttpClientExtensions
{
    /// <summary>
    /// Shared JsonSerializerOptions for consistent JSON handling across all operations.
    /// </summary>
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters =
        {
            new ISO8601DateTimeConverter(),
            new ISO8601DurationConverter()
        }
    };

    /// <summary>
    /// Serializes an object to JSON and creates a StringContent for HTTP requests.
    /// </summary>
    /// <typeparam name="T">The type of object to serialize.</typeparam>
    /// <param name="obj">The object to serialize.</param>
    /// <returns>A StringContent containing the JSON representation of the object.</returns>
    public static StringContent ToJsonContent<T>(this T obj)
    {
        var json = JsonSerializer.Serialize(obj, JsonOptions);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    /// <summary>
    /// Deserializes the HTTP content to an object of type T.
    /// </summary>
    /// <typeparam name="T">The type to deserialize to.</typeparam>
    /// <param name="content">The HTTP content to deserialize.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The deserialized object, or default if deserialization fails.</returns>
    public static async ValueTask<T?> ReadAsJsonAsync<T>(this HttpContent content, CancellationToken cancellationToken = default)
    {
        using var stream = await content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        return await JsonSerializer.DeserializeAsync<T>(stream, JsonOptions, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a GET HttpRequestMessage for the specified URL.
    /// </summary>
    /// <param name="url">The URL for the GET request.</param>
    /// <returns>A new HttpRequestMessage configured for a GET request.</returns>
    public static HttpRequestMessage CreateGetRequest(this string url) => new(HttpMethod.Get, url);

    /// <summary>
    /// Creates a POST HttpRequestMessage with a JSON body for the specified URL.
    /// </summary>
    /// <typeparam name="T">The type of the body object.</typeparam>
    /// <param name="url">The URL for the POST request.</param>
    /// <param name="body">The body object to be serialized to JSON.</param>
    /// <returns>A new HttpRequestMessage configured for a POST request with a JSON body.</returns>
    public static HttpRequestMessage CreatePostRequest<T>(this string url, T body) => new(HttpMethod.Post, url)
    {
        Content = body.ToJsonContent()
    };

    /// <summary>
    /// Sends an HTTP request and reads the response as JSON.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to.</typeparam>
    /// <param name="client">The HttpClient instance.</param>
    /// <param name="request">The HttpRequestMessage to send.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The deserialized response object.</returns>
    /// <exception cref="HttpRequestException">Thrown when the response is not successful.</exception>
    public static async Task<T> SendAndReadJsonAsync<T>(this HttpClient client, HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsJsonAsync<T>(cancellationToken: cancellationToken).ConfigureAwait(false)
            ?? throw new InvalidOperationException("Failed to deserialize the response.");
    }

    /// <summary>
    /// Adds a query parameter to a URL.
    /// </summary>
    /// <param name="url">The base URL.</param>
    /// <param name="name">The name of the query parameter.</param>
    /// <param name="value">The value of the query parameter.</param>
    /// <returns>The URL with the added query parameter.</returns>
    public static string AddQueryParam(this string url, string name, string value)
    {
        var separator = url.Contains('?') ? '&' : '?';
        return string.Create(url.Length + name.Length + value.Length + 2, (url, name, value, separator), (span, state) =>
        {
            state.url.AsSpan().CopyTo(span);
            span[state.url.Length] = state.separator;
            state.name.AsSpan().CopyTo(span[(state.url.Length + 1)..]);
            span[state.url.Length + state.name.Length + 1] = '=';
            Uri.EscapeDataString(state.value).AsSpan().CopyTo(span[(state.url.Length + state.name.Length + 2)..]);
        });
    }
}