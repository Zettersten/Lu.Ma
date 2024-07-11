using System.Text.Json.Serialization;

namespace Lu.Ma.Models.Shared;

/// <summary>
/// Represents a calendar entry.
/// </summary>
public sealed class CalendarEntry
{
    /// <summary>
    /// Gets or sets the API identifier for the calendar entry.
    /// </summary>
    [JsonPropertyName("api_id")]
    public string? ApiId { get; set; }

    /// <summary>
    /// Gets or sets the event associated with this calendar entry.
    /// </summary>
    [JsonPropertyName("event")]
    public CalendarEvent? Event { get; set; }

    /// <summary>
    /// Gets or sets the tags associated with this calendar entry.
    /// </summary>
    [JsonPropertyName("tags")]
    public CalendarEntryTag[]? Tags { get; set; }
}