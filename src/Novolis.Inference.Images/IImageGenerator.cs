namespace Novolis.Inference.Images;

/// <summary>Image generation provider contract.</summary>
public interface IImageGenerator
{
    /// <summary>Generates an image for a request.</summary>
    Task<GeneratedImage> GenerateAsync(
        ImageGenerationRequest request,
        CancellationToken cancellationToken = default);
}
