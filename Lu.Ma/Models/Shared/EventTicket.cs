using System.Text.Json.Serialization;

namespace Lu.Ma.Models.Shared;

/// <summary>
/// Represents an event ticket.
/// </summary>
public sealed class EventTicket
{
    /// <summary>
    /// Gets or sets the amount of the ticket.
    /// </summary>
    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    /// <summary>
    /// Gets or sets the discount amount for the ticket.
    /// </summary>
    [JsonPropertyName("amount_discount")]
    public int AmountDiscount { get; set; }

    /// <summary>
    /// Gets or sets the API identifier of the ticket.
    /// </summary>
    [JsonPropertyName("api_id")]
    public string? ApiId { get; set; }

    /// <summary>
    /// Gets or sets the currency of the ticket.
    /// </summary>
    [JsonPropertyName("currency")]
    public object? Currency { get; set; }

    /// <summary>
    /// Gets or sets the API identifier of the event ticket type.
    /// </summary>
    [JsonPropertyName("event_ticket_type_api_id")]
    public string? EventTicketTypeApiId { get; set; }

    /// <summary>
    /// Gets or sets the name of the ticket.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}