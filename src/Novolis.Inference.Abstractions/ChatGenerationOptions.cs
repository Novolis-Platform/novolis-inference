namespace Novolis.Inference.Abstractions;

/// <summary>Common sampling options for inference providers.</summary>
public sealed record ChatGenerationOptions
{
    /// <summary>Default options.</summary>
    public static ChatGenerationOptions Default { get; } = new();

    /// <summary>Maximum generated tokens.</summary>
    public int MaxTokens { get; init; } = 512;

    /// <summary>Sampling temperature.</summary>
    public float Temperature { get; init; } = 0.7f;

    /// <summary>Top-k sampling limit.</summary>
    public int TopK { get; init; } = 40;

    /// <summary>Top-p sampling threshold.</summary>
    public float TopP { get; init; } = 0.9f;

    /// <summary>Whether provider reasoning controls are enabled.</summary>
    public bool EnableThinking { get; init; }

    /// <summary>Whether reasoning content is surfaced.</summary>
    public bool ShowThinking { get; init; }

    /// <summary>Validates generic provider bounds.</summary>
    public void Validate()
    {
        if (MaxTokens <= 0) throw new ArgumentOutOfRangeException(nameof(MaxTokens));
        if (Temperature < 0) throw new ArgumentOutOfRangeException(nameof(Temperature));
        if (TopK < 0) throw new ArgumentOutOfRangeException(nameof(TopK));
        if (TopP is < 0 or > 1) throw new ArgumentOutOfRangeException(nameof(TopP));
    }
}
