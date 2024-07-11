using System.Text.Json.Serialization;

namespace Lu.Ma.Models.Shared;

/// <summary>
/// Represents geographical location information.
/// </summary>
public sealed class GeoLocation
{
    /// <summary>
    /// Gets or sets the type of the geographical location.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the place ID of the geographical location.
    /// </summary>
    [JsonPropertyName("place_id")]
    public string? PlaceId { get; set; }

    /// <summary>
    /// Gets or sets the address of the geographical location.
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the description of the geographical location.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }
}