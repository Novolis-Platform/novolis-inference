namespace Novolis.Inference.Images;

/// <summary>Provider-neutral image generation request.</summary>
public sealed record ImageGenerationRequest(
    string Prompt,
    int Width = 512,
    int Height = 512,
    int Steps = 20,
    int? Seed = null);
