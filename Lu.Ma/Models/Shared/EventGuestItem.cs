using System.Text.Json.Serialization;

namespace Lu.Ma.Models.Shared;

/// <summary>
/// Represents an individual guest to be added to an event.
/// </summary>
public sealed class EventGuestItem
{
    /// <summary>
    /// Gets or sets the email address of the guest.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the name of the guest.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}