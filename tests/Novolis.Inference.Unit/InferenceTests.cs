using System.Net;
using System.Net.Http.Json;
using Novolis.Inference.Abstractions;
using Novolis.Inference.Ollama;
using Novolis.Inference.Safety;

namespace Novolis.Inference.Unit;

public sealed class InferenceTests
{
    [Test]
    public async Task Ollama_model_maps_transcript_and_response()
    {
        var handler = new StubHandler();
        using var http = new HttpClient(handler) { BaseAddress = new Uri("http://localhost:11434/") };
        await using var model = new OllamaChatModel(http, new OllamaChatModelOptions { Model = "tiny" });

        var response = await model.GenerateAsync(
            [new ChatMessage(ChatMessageRole.User, "hello")]);

        await Assert.That(response.Content).IsEqualTo("hi");
        await Assert.That(handler.RequestBody).Contains("\"model\":\"tiny\"");
    }

    [Test]
    public async Task Safety_policy_applies_threshold()
    {
        var classifier = new ThresholdImageSafetyClassifier(
            (_, _) => Task.FromResult(0.75f),
            threshold: 0.5f);

        var result = await classifier.ClassifyAsync(new byte[] { 1, 2 });

        await Assert.That(result.IsAllowed).IsFalse();
        await Assert.That(result.RiskScore).IsEqualTo(0.75f);
    }

    private sealed class StubHandler : HttpMessageHandler
    {
        public string RequestBody { get; private set; } = string.Empty;

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            RequestBody = await request.Content!.ReadAsStringAsync(cancellationToken);
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(new
                {
                    message = new { role = "assistant", content = "hi" },
                    done = true,
                }),
            };
        }
    }
}
