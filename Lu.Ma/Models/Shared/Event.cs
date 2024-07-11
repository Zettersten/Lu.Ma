using System;
using System.Text.Json.Serialization;

namespace Lu.Ma.Models.Shared;

/// <summary>
/// Represents an event.
/// </summary>
public sealed class Event
{
    /// <summary>
    /// Gets or sets the API identifier of the event.
    /// </summary>
    [JsonPropertyName("api_id")]
    public string? ApiId { get; set; }

    /// <summary>
    /// Gets or sets the creation date and time of the event.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the URL of the event cover image.
    /// </summary>
    [JsonPropertyName("cover_url")]
    public string? CoverUrl { get; set; }

    /// <summary>
    /// Gets or sets the name of the event.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the API identifier of the series this event belongs to.
    /// </summary>
    [JsonPropertyName("series_api_id")]
    public object? SeriesApiId { get; set; }

    /// <summary>
    /// Gets or sets the description of the event.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the Markdown-formatted description of the event.
    /// </summary>
    [JsonPropertyName("description_md")]
    public string? DescriptionMd { get; set; }

    /// <summary>
    /// Gets or sets the start date and time of the event.
    /// </summary>
    [JsonPropertyName("start_at")]
    public DateTime StartAt { get; set; }

    /// <summary>
    /// Gets or sets the end date and time of the event.
    /// </summary>
    [JsonPropertyName("end_at")]
    public DateTime EndAt { get; set; }

    /// <summary>
    /// Gets or sets the JSON representation of the event's geographical address.
    /// </summary>
    [JsonPropertyName("geo_address_json")]
    public object? GeoAddressJson { get; set; }

    /// <summary>
    /// Gets or sets the latitude of the event's location.
    /// </summary>
    [JsonPropertyName("geo_latitude")]
    public object? GeoLatitude { get; set; }

    /// <summary>
    /// Gets or sets the longitude of the event's location.
    /// </summary>
    [JsonPropertyName("geo_longitude")]
    public object? GeoLongitude { get; set; }

    /// <summary>
    /// Gets or sets the URL of the event.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Gets or sets the URL of the social media image for the event.
    /// </summary>
    [JsonPropertyName("social_image_url")]
    public string? SocialImageUrl { get; set; }

    /// <summary>
    /// Gets or sets the timezone of the event.
    /// </summary>
    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    /// <summary>
    /// Gets or sets the type of the event.
    /// </summary>
    [JsonPropertyName("event_type")]
    public string? EventType { get; set; }

    /// <summary>
    /// Gets or sets the visibility setting of the event.
    /// </summary>
    [JsonPropertyName("visibility")]
    public string? Visibility { get; set; }

    /// <summary>
    /// Gets or sets the URL for the event meeting.
    /// </summary>
    [JsonPropertyName("meeting_url")]
    public object? MeetingUrl { get; set; }

    /// <summary>
    /// Gets or sets the Zoom meeting URL for the event.
    /// </summary>
    [JsonPropertyName("zoom_meeting_url")]
    public object? ZoomMeetingUrl { get; set; }
}