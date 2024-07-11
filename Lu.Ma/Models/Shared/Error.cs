using System.Text.Json.Serialization;

namespace Lu.Ma.Models.Shared;

public sealed class Error
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("code")]
    public object? Code { get; set; }
}