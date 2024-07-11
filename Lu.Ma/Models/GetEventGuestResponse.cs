using Lu.Ma.Models.Shared;
using System.Text.Json.Serialization;

namespace Lu.Ma.Models;

/// <summary>
/// Represents the paginated response for getting event guests.
/// </summary>
public sealed class GetEventGuestResponse : PaginatedBased
{
    /// <summary>
    /// Gets or sets the list of event entries.
    /// </summary>
    [JsonPropertyName("entries")]
    public List<EventEntry>? Entries { get; set; }
}