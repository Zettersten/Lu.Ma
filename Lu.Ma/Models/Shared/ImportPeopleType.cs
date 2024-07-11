using System.Text.Json.Serialization;

namespace Lu.Ma.Models.Shared;

/// <summary>
/// Represents the information for a person to be imported.
/// </summary>
public sealed class ImportPeopleType
{
    /// <summary>
    /// Gets or sets the email address of the person.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the name of the person.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
}