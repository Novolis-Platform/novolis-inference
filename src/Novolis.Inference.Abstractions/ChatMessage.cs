namespace Novolis.Inference.Abstractions;

/// <summary>Provider-neutral chat message.</summary>
public sealed record ChatMessage(
    ChatMessageRole Role,
    string Content,
    string? Name = null,
    IReadOnlyDictionary<string, string>? Metadata = null);
