using Lu.Ma.Models.Shared;
using System.Text.Json.Serialization;

namespace Lu.Ma.Models;

/// <summary>
/// Represents an event to be created.
/// </summary>
public sealed class CreateEventRequest
{
    /// <summary>
    /// Gets or sets the name of the event.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the start time of the event.
    /// </summary>
    [JsonPropertyName("start_at")]
    public DateTime StartAt { get; set; }

    /// <summary>
    /// Gets or sets the timezone of the event.
    /// </summary>
    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    /// <summary>
    /// Gets or sets the end time of the event.
    /// </summary>
    [JsonPropertyName("end_at")]
    public DateTime EndAt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether RSVP approval is required.
    /// </summary>
    [JsonPropertyName("require_rsvp_approval")]
    public bool RequireRsvpApproval { get; set; }

    /// <summary>
    /// Gets or sets the URL for the meeting.
    /// </summary>
    [JsonPropertyName("meeting_url")]
    public string? MeetingUrl { get; set; }

    /// <summary>
    /// Gets or sets the geographical address information.
    /// </summary>
    [JsonPropertyName("geo_address_json")]
    public GeoLocation? GeoAddressJson { get; set; }

    /// <summary>
    /// Gets or sets the latitude of the event location.
    /// </summary>
    [JsonPropertyName("geo_latitude")]
    public string? GeoLatitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude of the event location.
    /// </summary>
    [JsonPropertyName("geo_longitude")]
    public string? GeoLongitude { get; set; }
}