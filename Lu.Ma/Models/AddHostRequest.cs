using System.Text.Json.Serialization;

namespace Lu.Ma.Models;

/// <summary>
/// Represents a request to add a host to an event.
/// </summary>
public sealed class AddHostRequest
{
    /// <summary>
    /// Gets or sets the API identifier of the event.
    /// </summary>
    [JsonPropertyName("event_api_id")]
    public string? EventApiId { get; set; }

    /// <summary>
    /// Gets or sets the email address of the host.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the access level for the host.
    /// </summary>
    [JsonPropertyName("access_level")]
    public string? AccessLevel { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the host is visible.
    /// </summary>
    [JsonPropertyName("is_visible")]
    public bool IsVisible { get; set; }

    /// <summary>
    /// Gets or sets the name of the host.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}