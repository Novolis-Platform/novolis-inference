namespace Novolis.Inference.Abstractions;

/// <summary>Capabilities exposed by an inference backend.</summary>
public sealed record LanguageModelCapabilities
{
    /// <summary>Chat-only capability set.</summary>
    public static LanguageModelCapabilities ChatOnly { get; } = new() { SupportsChat = true };

    /// <summary>Whether chat generation is supported.</summary>
    public bool SupportsChat { get; init; }

    /// <summary>Whether token streaming callbacks are supported.</summary>
    public bool SupportsTokenStreaming { get; init; } = true;

    /// <summary>Whether local model files can be loaded.</summary>
    public bool SupportsLocalModelFiles { get; init; }
}
