using Novolis.Inference.Abstractions;

namespace Novolis.Inference.SharpMind;

/// <summary>Options for loading a SharpMind-backed local chat model.</summary>
public sealed record SharpMindChatModelOptions
{
    /// <summary>Path to a SharpMind-supported model file.</summary>
    public required string ModelPath { get; init; }

    /// <summary>Optional external tokenizer path.</summary>
    public string? TokenizerPath { get; init; }

    /// <summary>Model loading strategy.</summary>
    public SharpMindModelLoadMode LoadMode { get; init; } = SharpMindModelLoadMode.Full;

    /// <summary>Hardware tier override.</summary>
    public SharpMindHardwareTier HardwareTier { get; init; } = SharpMindHardwareTier.Auto;

    /// <summary>Optional display name.</summary>
    public string? Name { get; init; }

    /// <summary>Optional generator seed.</summary>
    public int? Seed { get; init; }

    /// <summary>Default generation options.</summary>
    public ChatGenerationOptions GenerationOptions { get; init; } = ChatGenerationOptions.Default;

    /// <summary>Validates options.</summary>
    public void Validate()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(ModelPath);
        GenerationOptions.Validate();
    }
}
