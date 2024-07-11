using System.Text.Json.Serialization;

namespace Lu.Ma.Models.Shared;

/// <summary>
/// Represents an entry for an event.
/// </summary>
public sealed class EventEntry
{
    /// <summary>
    /// Gets or sets the API identifier of the event entry.
    /// </summary>
    [JsonPropertyName("api_id")]
    public string? ApiId { get; set; }

    /// <summary>
    /// Gets or sets the guest associated with this event entry.
    /// </summary>
    [JsonPropertyName("guest")]
    public EventGuest? Guest { get; set; }
}