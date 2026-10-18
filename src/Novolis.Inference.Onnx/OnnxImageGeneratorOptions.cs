namespace Novolis.Inference.Onnx;

/// <summary>Options for loading an ONNX image model.</summary>
public sealed record OnnxImageGeneratorOptions
{
    /// <summary>Path to the ONNX model or model graph.</summary>
    public required string ModelPath { get; init; }

    /// <summary>Whether the runtime should use graph optimization.</summary>
    public bool EnableGraphOptimization { get; init; } = true;
}
