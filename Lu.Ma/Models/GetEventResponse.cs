using Lu.Ma.Models.Shared;
using System.Text.Json.Serialization;

namespace Lu.Ma.Models;

/// <summary>
/// Represents the response for getting an event.
/// </summary>
public sealed class GetEventResponse
{
    /// <summary>
    /// Gets or sets the event details.
    /// </summary>
    [JsonPropertyName("event")]
    public CalendarEvent? Event { get; set; }

    /// <summary>
    /// Gets or sets the list of event hosts.
    /// </summary>
    [JsonPropertyName("hosts")]
    public List<EventHost>? Hosts { get; set; }
}