using System.Text.Json.Serialization;

namespace Lu.Ma.Models;

/// <summary>
/// Represents a request to update a coupon.
/// </summary>
public sealed class UpdateCouponRequest
{
    /// <summary>
    /// Gets or sets the coupon code to be updated.
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; set; }

    /// <summary>
    /// Gets or sets the new remaining count for the coupon.
    /// </summary>
    [JsonPropertyName("remaining_count")]
    public int RemainingCount { get; set; }
}