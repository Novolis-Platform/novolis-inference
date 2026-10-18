namespace Novolis.Inference.Abstractions;

/// <summary>Role of a message in a chat transcript.</summary>
public enum ChatMessageRole
{
    /// <summary>Instruction from the application.</summary>
    System,

    /// <summary>Input from the caller.</summary>
    User,

    /// <summary>Generated model response.</summary>
    Assistant,
}
