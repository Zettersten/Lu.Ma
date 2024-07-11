using System.Text.Json.Serialization;

namespace Lu.Ma.Models.Shared;

/// <summary>
/// Represents a tag associated with a calendar entry.
/// </summary>
public sealed class CalendarEntryTag
{
    /// <summary>
    /// Gets or sets the API identifier for the tag.
    /// </summary>
    [JsonPropertyName("api_id")]
    public string? ApiId { get; set; }

    /// <summary>
    /// Gets or sets the name of the tag.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}