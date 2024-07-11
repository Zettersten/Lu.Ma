using Lu.Ma.Models.Shared;
using System.Text.Json.Serialization;

namespace Lu.Ma.Models;

/// <summary>
/// Represents a request to add guests to an event.
/// </summary>
public sealed class AddGuestRequest
{
    /// <summary>
    /// Gets or sets the list of guests to be added.
    /// </summary>
    [JsonPropertyName("guests")]
    public List<EventGuestItem>? Guests { get; set; }

    /// <summary>
    /// Gets or sets the API identifier of the event.
    /// </summary>
    [JsonPropertyName("event_api_id")]
    public string? EventApiId { get; set; }
}