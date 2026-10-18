namespace Novolis.Inference.Abstractions;

/// <summary>Provider-neutral chat inference model.</summary>
public interface IChatModel : IAsyncDisposable
{
    /// <summary>Display name for diagnostics and UI.</summary>
    string Name { get; }

    /// <summary>Capabilities exposed by the provider.</summary>
    LanguageModelCapabilities Capabilities { get; }

    /// <summary>Generates one assistant response.</summary>
    Task<ChatResponse> GenerateAsync(
        IReadOnlyList<ChatMessage> messages,
        ChatGenerationOptions? options = null,
        IProgress<string>? tokenProgress = null,
        CancellationToken cancellationToken = default);
}
