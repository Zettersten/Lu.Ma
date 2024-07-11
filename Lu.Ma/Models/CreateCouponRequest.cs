namespace Lu.Ma.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Represents a request to create a coupon for an event.
/// </summary>
public sealed class CreateCouponRequest
{
    /// <summary>
    /// Gets or sets the API identifier of the event.
    /// </summary>
    [JsonPropertyName("event_api_id")]
    public string? EventApiId { get; set; }

    /// <summary>
    /// Gets or sets the coupon code.
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    /// <summary>
    /// Gets or sets the remaining count of the coupon.
    /// </summary>
    [JsonPropertyName("remaining_count")]
    public int RemainingCount { get; set; }

    /// <summary>
    /// Gets or sets the type of discount for the coupon.
    /// </summary>
    [JsonPropertyName("discount_type")]
    public string? DiscountType { get; set; }

    /// <summary>
    /// Gets or sets the percentage off for the coupon.
    /// </summary>
    [JsonPropertyName("percent_off")]
    public int PercentOff { get; set; }

    /// <summary>
    /// Gets or sets the cents off for the coupon.
    /// </summary>
    [JsonPropertyName("cents_off")]
    public int CentsOff { get; set; }

    /// <summary>
    /// Gets or sets the currency for the coupon.
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }
}