namespace Novolis.Inference.SharpMind;

/// <summary>Model loading strategy for SharpMind.</summary>
public enum SharpMindModelLoadMode
{
    /// <summary>Load the model fully before inference.</summary>
    Full,
    /// <summary>Stream model data as it is needed.</summary>
    Streaming,
}
