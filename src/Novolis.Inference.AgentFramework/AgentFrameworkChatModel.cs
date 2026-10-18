using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Novolis.Inference.Abstractions;
using InferenceChatMessage = Novolis.Inference.Abstractions.ChatMessage;
using InferenceChatResponse = Novolis.Inference.Abstractions.ChatResponse;
using InferenceChatRole = Novolis.Inference.Abstractions.ChatMessageRole;
using FrameworkChatMessage = Microsoft.Extensions.AI.ChatMessage;
using FrameworkChatRole = Microsoft.Extensions.AI.ChatRole;

namespace Novolis.Inference.AgentFramework;

/// <summary>Adapts a Microsoft Agent Framework chat client to Novolis inference contracts.</summary>
public sealed class AgentFrameworkChatModel : IChatModel
{
    private readonly ChatClientAgent agent;
    private readonly IDisposable? ownedClient;

    /// <summary>Creates an adapter over an existing Agent Framework chat agent.</summary>
    public AgentFrameworkChatModel(ChatClientAgent agent, string? name = null)
    {
        ArgumentNullException.ThrowIfNull(agent);
        this.agent = agent;
        Name = string.IsNullOrWhiteSpace(name)
            ? string.IsNullOrWhiteSpace(agent.Name) ? "Agent Framework" : agent.Name
            : name;
    }

    /// <summary>Creates an Agent Framework chat agent over an AI chat client.</summary>
    public AgentFrameworkChatModel(IChatClient client, string? name = null)
    {
        ArgumentNullException.ThrowIfNull(client);
        ownedClient = client;
        agent = new ChatClientAgent(client, name: name);
        Name = string.IsNullOrWhiteSpace(name) ? "Agent Framework" : name;
    }

    /// <inheritdoc />
    public string Name { get; }

    /// <inheritdoc />
    public LanguageModelCapabilities Capabilities { get; } = LanguageModelCapabilities.ChatOnly;

    /// <inheritdoc />
    public async Task<InferenceChatResponse> GenerateAsync(
        IReadOnlyList<InferenceChatMessage> messages,
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
        var builder = new System.Text.StringBuilder();
        await foreach (var update in agent.RunStreamingAsync(
                           messages.Select(MapMessage),
                           session: null,
                           options: new ChatClientAgentRunOptions
            {
                ChatOptions = new ChatOptions
                {
                    MaxOutputTokens = generation.MaxTokens,
                    Temperature = generation.Temperature,
                    TopP = generation.TopP,
                },
            },
                           cancellationToken).ConfigureAwait(false))
        {
            if (update.Text is not { Length: > 0 } token)
            {
                continue;
            }

            builder.Append(token);
            tokenProgress?.Report(token);
        }

        var content = builder.ToString();
        return new InferenceChatResponse(
            content,
            [.. messages, new InferenceChatMessage(InferenceChatRole.Assistant, content)]);
    }

    /// <inheritdoc />
    public ValueTask DisposeAsync()
    {
        ownedClient?.Dispose();
        return ValueTask.CompletedTask;
    }

    private static FrameworkChatMessage MapMessage(InferenceChatMessage message) =>
        new(
            message.Role switch
            {
                InferenceChatRole.System => FrameworkChatRole.System,
                InferenceChatRole.User => FrameworkChatRole.User,
                InferenceChatRole.Assistant => FrameworkChatRole.Assistant,
                _ => throw new ArgumentOutOfRangeException(nameof(message)),
            },
            message.Content);
}
