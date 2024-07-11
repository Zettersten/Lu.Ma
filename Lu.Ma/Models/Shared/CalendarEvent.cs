using System.Text.Json.Serialization;

namespace Lu.Ma.Models.Shared;

/// <summary>
/// Represents a calendar event.
/// </summary>
public sealed class CalendarEvent
{
    /// <summary>
    /// Gets or sets the API identifier for the event.
    /// </summary>
    [JsonPropertyName("api_id")]
    public string? ApiId { get; set; }

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
    /// Gets or sets the API identifier of the series this event belongs to, if any.
    /// </summary>
    [JsonPropertyName("series_api_id")]
    public object? SeriesApiId { get; set; }

    /// <summary>
    /// Gets or sets the start time of the event.
    /// </summary>
    [JsonPropertyName("start_at")]
    public DateTime StartAt { get; set; }

    /// <summary>
    /// Gets or sets the end time of the event.
    /// </summary>
    [JsonPropertyName("end_at")]
    public DateTime EndAt { get; set; }

    /// <summary>
    /// Gets or sets the URL of the event.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Gets or sets the URL of the social image for the event.
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
    /// Gets or sets the visibility status of the event.
    /// </summary>
    [JsonPropertyName("visibility")]
    public string? Visibility { get; set; }
}