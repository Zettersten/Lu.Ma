using Lu.Ma.Models.Shared;
using System.Text.Json.Serialization;

namespace Lu.Ma.Models;

/// <summary>
/// Represents a request to update the status of a guest for an event.
/// </summary>
public sealed class UpdateGuestStatusRequest
{
    /// <summary>
    /// Gets or sets the API identifier of the event.
    /// </summary>
    [JsonPropertyName("event_api_id")]
    public string? EventApiId { get; set; }

    /// <summary>
    /// Gets or sets the guest whose status is to be updated.
    /// </summary>
    [JsonPropertyName("guest")]
    public EventGuestItem? Guest { get; set; }

    /// <summary>
    /// Gets or sets the new status for the guest.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether a refund should be issued.
    /// </summary>
    [JsonPropertyName("should_refund")]
    public bool ShouldRefund { get; set; }
}