namespace Novolis.Inference.Abstractions;

/// <summary>Result from a chat generation request.</summary>
public sealed record ChatResponse(
    string Content,
    IReadOnlyList<ChatMessage> History,
    float? TimeToFirstToken = null,
    float? TokensPerSecond = null);
