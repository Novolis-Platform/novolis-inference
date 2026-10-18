namespace Novolis.Inference.Safety;

/// <summary>Safety classification result for image bytes.</summary>
public sealed record ImageSafetyResult(
    bool IsAllowed,
    float RiskScore,
    string? Category = null,
    string? Explanation = null);
