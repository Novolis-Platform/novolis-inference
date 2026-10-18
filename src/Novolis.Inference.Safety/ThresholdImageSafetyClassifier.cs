namespace Novolis.Inference.Safety;

/// <summary>Policy wrapper around a provider classifier score.</summary>
public sealed class ThresholdImageSafetyClassifier(
    Func<ReadOnlyMemory<byte>, CancellationToken, Task<float>> scoreProvider,
    float threshold = 0.5f) : IImageSafetyClassifier
{
    /// <inheritdoc />
    public async Task<ImageSafetyResult> ClassifyAsync(
        ReadOnlyMemory<byte> image,
        CancellationToken cancellationToken = default)
    {
        if (threshold is < 0 or > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(threshold));
        }

        var score = Math.Clamp(
            await scoreProvider(image, cancellationToken),
            0,
            1);
        var allowed = score < threshold;
        return new ImageSafetyResult(
            allowed,
            score,
            allowed ? null : "policy",
            allowed ? "Image is below the configured risk threshold." : "Image exceeded the configured risk threshold.");
    }
}
