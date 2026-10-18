namespace Novolis.Inference.Ollama;

/// <summary>Connection and model options for an Ollama server.</summary>
public sealed record OllamaChatModelOptions
{
    /// <summary>Ollama endpoint.</summary>
    public Uri Endpoint { get; init; } = new("http://localhost:11434/");

    /// <summary>Model name served by Ollama.</summary>
    public required string Model { get; init; }

    /// <summary>Display name override.</summary>
    public string? Name { get; init; }
}
