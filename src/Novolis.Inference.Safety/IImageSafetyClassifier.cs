namespace Novolis.Inference.Safety;

/// <summary>Classifies generated image content against an application policy.</summary>
public interface IImageSafetyClassifier
{
    /// <summary>Classifies image bytes.</summary>
    Task<ImageSafetyResult> ClassifyAsync(
        ReadOnlyMemory<byte> image,
        CancellationToken cancellationToken = default);
}
