using Lu.Ma.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;

namespace Lu.Ma.Extensions;

/// <summary>
/// Provides extension methods for HttpClient to simplify JSON-based API interactions.
/// </summary>
public static class HttpClientExtensions
{
    /// <summary>
    /// Shared JsonSerializerOptions for consistent JSON handling across all operations.
    /// </summary>
    public static readonly JsonSerializerOptions JsonOptions = new()
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
    [UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.", Justification = "<Pending>")]
    [UnconditionalSuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code", Justification = "<Pending>")]
    public static StringContent ToJsonContent<T>(this T obj)
    {
        var json = JsonSerializer.Serialize(obj, JsonOptions) ?? string.Empty;
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    /// <summary>
    /// Deserializes the HTTP content to an object of type T.
    /// </summary>
    /// <typeparam name="T">The type to deserialize to.</typeparam>
    /// <param name="content">The HTTP content to deserialize.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The deserialized object, or default if deserialization fails.</returns>
    [UnconditionalSuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code", Justification = "<Pending>")]
    [UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.", Justification = "<Pending>")]
    public static async ValueTask<T?> ReadAsJsonAsync<T>(this HttpContent content, CancellationToken cancellationToken = default)
    {
        using var stream = await content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        return await JsonSerializer.DeserializeAsync<T>(stream, JsonOptions, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Adds a query parameter to a URL.
    /// </summary>
    /// <param name="url">The base URL.</param>
    /// <param name="name">The name of the query parameter.</param>
    /// <param name="value">The value of the query parameter.</param>
    /// <returns>The URL with the added query parameter.</returns>
    public static string AddQueryParam(this string? url, string? name, string? value)
    {
        if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
        {
            return url ?? string.Empty;
        }

        var separator = url.Contains('?') ? '&' : '?';
        var encodedValue = Uri.EscapeDataString(value);

        return string.Concat(url, separator, name, "=", encodedValue);
    }
}