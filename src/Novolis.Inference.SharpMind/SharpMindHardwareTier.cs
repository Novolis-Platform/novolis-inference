namespace Novolis.Inference.SharpMind;

/// <summary>Hardware instruction-set preference for SharpMind.</summary>
public enum SharpMindHardwareTier
{
    /// <summary>Choose the best supported hardware tier automatically.</summary>
    Auto,
    /// <summary>Use FMA instructions.</summary>
    Fma,
    /// <summary>Use AVX2 instructions.</summary>
    Avx2,
    /// <summary>Use SSE instructions.</summary>
    Sse,
    /// <summary>Use scalar operations.</summary>
    Scalar,
}
