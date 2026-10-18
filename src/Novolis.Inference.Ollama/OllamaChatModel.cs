using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Novolis.Inference.Abstractions;

namespace Novolis.Inference.Ollama;

/// <summary>Ollama chat adapter using the provider's HTTP API.</summary>
public sealed class OllamaChatModel : IChatModel
{
    private readonly HttpClient _httpClient;
    private readonly OllamaChatModelOptions _options;

    /// <summary>Creates an Ollama adapter.</summary>
    public OllamaChatModel(HttpClient httpClient, OllamaChatModelOptions options)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _httpClient.BaseAddress ??= options.Endpoint;
        Name = string.IsNullOrWhiteSpace(options.Name) ? options.Model : options.Name;
    }

    /// <inheritdoc />
    public string Name { get; }

    /// <inheritdoc />
    public LanguageModelCapabilities Capabilities { get; } = new()
    {
        SupportsChat = true,
        SupportsLocalModelFiles = false,
        SupportsTokenStreaming = false,
    };

    /// <inheritdoc />
    public async Task<ChatResponse> GenerateAsync(
        IReadOnlyList<ChatMessage> messages,
        ChatGenerationOptions? options = null,
        IProgress<string>? tokenProgress = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(messages);
        if (messages.Count == 0)
        {
            throw new ArgumentException("At least one message is required.", nameof(messages));
        }

        var generation = options ?? ChatGenerationOptions.Default;
        generation.Validate();
        var payload = new OllamaRequest(
            _options.Model,
            messages.Select(message => new OllamaMessage(
                message.Role switch
                {
                    ChatMessageRole.System => "system",
                    ChatMessageRole.User => "user",
                    ChatMessageRole.Assistant => "assistant",
                    _ => throw new ArgumentOutOfRangeException(),
                },
                message.Content)).ToArray(),
            Stream: false,
            Options: new OllamaGenerationOptions(generation.Temperature, generation.TopP));

        using var response = await _httpClient.PostAsJsonAsync("api/chat", payload, cancellationToken);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<OllamaResponse>(cancellationToken);
        var content = result?.Message?.Content ?? string.Empty;
        tokenProgress?.Report(content);

        return new ChatResponse(
            content,
            [.. messages, new ChatMessage(ChatMessageRole.Assistant, content)]);
    }

    /// <inheritdoc />
    public ValueTask DisposeAsync() => ValueTask.CompletedTask;

    private sealed record OllamaRequest(
        string Model,
        IReadOnlyList<OllamaMessage> Messages,
        bool Stream,
        OllamaGenerationOptions Options);

    private sealed record OllamaMessage(string Role, string Content);

    private sealed record OllamaGenerationOptions(
        [property: JsonPropertyName("temperature")] float Temperature,
        [property: JsonPropertyName("top_p")] float TopP);

    private sealed record OllamaResponse(OllamaMessage? Message);
}
