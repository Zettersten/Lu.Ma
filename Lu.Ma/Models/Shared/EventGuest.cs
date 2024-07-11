using System.Text.Json.Serialization;

namespace Lu.Ma.Models.Shared;

/// <summary>
/// Represents a guest for an event.
/// </summary>
public sealed class EventGuest
{
    /// <summary>
    /// Gets or sets the API identifier of the event guest.
    /// </summary>
    [JsonPropertyName("api_id")]
    public string? ApiId { get; set; }

    /// <summary>
    /// Gets or sets the approval status of the event guest.
    /// </summary>
    [JsonPropertyName("approval_status")]
    public string? ApprovalStatus { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the guest registered for the event.
    /// </summary>
    [JsonPropertyName("registered_at")]
    public DateTime? RegisteredAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the guest was invited to the event.
    /// </summary>
    [JsonPropertyName("invited_at")]
    public DateTime? InvitedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the guest checked in to the event.
    /// </summary>
    [JsonPropertyName("checked_in_at")]
    public object? CheckedInAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the guest joined the event.
    /// </summary>
    [JsonPropertyName("joined_at")]
    public object? JoinedAt { get; set; }

    /// <summary>
    /// Gets or sets the API identifier of the user associated with this guest.
    /// </summary>
    [JsonPropertyName("user_api_id")]
    public string? UserApiId { get; set; }

    /// <summary>
    /// Gets or sets the creation date and time of the event guest record.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the name of the user associated with this guest.
    /// </summary>
    [JsonPropertyName("user_name")]
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the email of the user associated with this guest.
    /// </summary>
    [JsonPropertyName("user_email")]
    public string? UserEmail { get; set; }

    /// <summary>
    /// Gets or sets the list of registration answers provided by the guest.
    /// </summary>
    [JsonPropertyName("registration_answers")]
    public List<EventRegistrationAnswers>? RegistrationAnswers { get; set; }

    /// <summary>
    /// Gets or sets the QR code for guest check-in.
    /// </summary>
    [JsonPropertyName("check_in_qr_code")]
    public string? CheckInQrCode { get; set; }

    /// <summary>
    /// Gets or sets the event ticket associated with this guest.
    /// </summary>
    [JsonPropertyName("event_ticket")]
    public EventTicket? EventTicket { get; set; }
}