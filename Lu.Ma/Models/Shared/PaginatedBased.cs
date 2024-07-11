using System.Text.Json.Serialization;

namespace Lu.Ma.Models.Shared;

/// <summary>
/// Represents the base class for paginated responses.
/// </summary>
public abstract class PaginatedBased
{
    /// <summary>
    /// Gets or sets a value indicating whether there are more items available.
    /// </summary>
    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }

    /// <summary>
    /// Gets or sets the cursor for the next page of results.
    /// </summary>
    [JsonPropertyName("next_cursor")]
    public string? NextCursor { get; set; }
}