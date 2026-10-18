using Microsoft.ML.OnnxRuntime;
using Novolis.Inference.Images;

namespace Novolis.Inference.Onnx;

/// <summary>ONNX Runtime image generator with an explicit graph mapping pipeline.</summary>
public sealed class OnnxImageGenerator : IImageGenerator, IDisposable
{
    private readonly InferenceSession _session;
    private readonly IOnnxImagePipeline _pipeline;

    /// <summary>Loads an ONNX graph and selects its tensor mapping pipeline.</summary>
    public OnnxImageGenerator(OnnxImageGeneratorOptions options, IOnnxImagePipeline pipeline)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(pipeline);
        if (!File.Exists(options.ModelPath))
        {
            throw new FileNotFoundException("ONNX model was not found.", options.ModelPath);
        }

        _session = new InferenceSession(options.ModelPath);
        _pipeline = pipeline;
    }

    /// <inheritdoc />
    public Task<GeneratedImage> GenerateAsync(
        ImageGenerationRequest request,
        CancellationToken cancellationToken = default) =>
        _pipeline.GenerateAsync(_session, request, cancellationToken);

    /// <inheritdoc />
    public void Dispose() => _session.Dispose();
}
