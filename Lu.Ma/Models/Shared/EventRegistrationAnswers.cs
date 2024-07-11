using System.Text.Json.Serialization;

namespace Lu.Ma.Models.Shared;

/// <summary>
/// Represents answers to event registration questions.
/// </summary>
public sealed class EventRegistrationAnswers
{
    /// <summary>
    /// Gets or sets the label of the registration question.
    /// </summary>
    [JsonPropertyName("label")]
    public string? Label { get; set; }

    /// <summary>
    /// Gets or sets the answer to the registration question.
    /// </summary>
    [JsonPropertyName("answer")]
    public string? Answer { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the question.
    /// </summary>
    [JsonPropertyName("question_id")]
    public string? QuestionId { get; set; }

    /// <summary>
    /// Gets or sets the type of the question.
    /// </summary>
    [JsonPropertyName("question_type")]
    public string? QuestionType { get; set; }
}