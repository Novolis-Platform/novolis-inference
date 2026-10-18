# Getting started

For provider-neutral chat APIs:

```powershell
dotnet add package Novolis.Inference.Abstractions
```

For an Agent Framework-compatible client:

```powershell
dotnet add package Novolis.Inference.AgentFramework
```

```csharp
var model = new AgentFrameworkChatModel(chatClient, "local agent");
var response = await model.GenerateAsync(
    [new ChatMessage(ChatMessageRole.User, "Hello")],
    cancellationToken: cancellationToken);
```

Image providers expose `IImageGenerator` and can be passed through `IImageSafetyClassifier` before a host persists or displays the result.
