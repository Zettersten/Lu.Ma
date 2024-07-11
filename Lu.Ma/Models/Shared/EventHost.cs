using System.Text.Json.Serialization;

namespace Lu.Ma.Models.Shared;


/// <summary>
/// Represents an event host.
/// </summary>
public sealed class EventHost
{
    /// <summary>
    /// Gets or sets the API identifier of the event host.
    /// </summary>
    [JsonPropertyName("api_id")]
    public string? ApiId { get; set; }

    /// <summary>
    /// Gets or sets the email address of the event host.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the name of the event host.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the URL of the event host's avatar.
    /// </summary>
    [JsonPropertyName("avatar_url")]
    public string? AvatarUrl { get; set; }
}