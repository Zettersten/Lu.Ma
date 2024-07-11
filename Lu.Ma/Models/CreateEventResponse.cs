using System.Text.Json.Serialization;

namespace Lu.Ma.Models;

/// <summary>
/// Represents the response received after creating an event.
/// </summary>
public sealed class CreateEventResponse
{
    /// <summary>
    /// Gets or sets the API identifier of the created event.
    /// </summary>
    [JsonPropertyName("api_id")]
    public string? ApiId { get; set; }
}