using Microsoft.ML.OnnxRuntime;
using Novolis.Inference.Images;

namespace Novolis.Inference.Onnx;

/// <summary>Model-specific tensor mapping for an ONNX image graph.</summary>
public interface IOnnxImagePipeline
{
    /// <summary>Runs the graph and maps output tensors to an image.</summary>
    Task<GeneratedImage> GenerateAsync(
        InferenceSession session,
        ImageGenerationRequest request,
        CancellationToken cancellationToken = default);
}
