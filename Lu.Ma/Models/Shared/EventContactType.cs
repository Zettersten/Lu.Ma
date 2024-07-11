using System.Text.Json.Serialization;

namespace Lu.Ma.Models.Shared;

/// <summary>
/// Represents contact information for an event.
/// </summary>
public sealed class EventContactType
{
    /// <summary>
    /// Gets or sets the type of contact.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the email address for the contact.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }
}