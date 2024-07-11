using Lu.Ma.Models.Shared;
using System.Text.Json.Serialization;

namespace Lu.Ma.Models;

/// <summary>
/// Represents a request to import people into the system.
/// </summary>
public sealed class ImportPeopleRequest
{
    /// <summary>
    /// Gets or sets the list of people information to import.
    /// </summary>
    [JsonPropertyName("infos")]
    public List<ImportPeopleType>? Infos { get; set; }

    /// <summary>
    /// Gets or sets the list of tag API identifiers to associate with the imported people.
    /// </summary>
    [JsonPropertyName("tag_api_ids")]
    public List<string>? TagApiIds { get; set; }
}