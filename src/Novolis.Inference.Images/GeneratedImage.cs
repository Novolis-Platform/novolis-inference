namespace Novolis.Inference.Images;

/// <summary>Generated image bytes and provider metadata.</summary>
public sealed record GeneratedImage(
    ReadOnlyMemory<byte> Data,
    string ContentType,
    int Width,
    int Height,
    int? Seed = null);
