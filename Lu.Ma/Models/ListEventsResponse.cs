using Lu.Ma.Models.Shared;
using System.Text.Json.Serialization;

namespace Lu.Ma.Models;

/// <summary>
/// Represents a response containing a list of events.
/// </summary>
public sealed class ListEventsResponse : PaginatedBased
{
    /// <summary>
    /// Gets or sets the list of calendar entries.
    /// </summary>
    [JsonPropertyName("entries")]
    public List<CalendarEntry>? Entries { get; set; }
}