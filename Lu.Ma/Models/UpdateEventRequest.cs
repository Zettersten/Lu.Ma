using Lu.Ma.Models.Shared;
using System.Text.Json.Serialization;

namespace Lu.Ma.Models;

/// <summary>
/// Represents a request to update an event.
/// </summary>
public sealed class UpdateEventRequest
{
    /// <summary>
    /// Gets or sets the API identifier of the event to be updated.
    /// </summary>
    [JsonPropertyName("event_api_id")]
    public string? EventApiId { get; set; }

    /// <summary>
    /// Gets or sets the new name for the event.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the new visibility setting for the event.
    /// </summary>
    [JsonPropertyName("visibility")]
    public string? Visibility { get; set; }

    /// <summary>
    /// Gets or sets the new geographical address for the event in JSON format.
    /// </summary>
    [JsonPropertyName("geo_address_json")]
    public EventContactType? GeoAddressJson { get; set; }

    /// <summary>
    /// Gets or sets the new latitude for the event location.
    /// </summary>
    [JsonPropertyName("geo_latitude")]
    public string? GeoLatitude { get; set; }

    /// <summary>
    /// Gets or sets the new longitude for the event location.
    /// </summary>
    [JsonPropertyName("geo_longitude")]
    public string? GeoLongitude { get; set; }
}