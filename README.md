# novolis-inference

Inference consumes and runs models. Training and algorithm development remain in [`novolis-machinelearning`](d:\novolis\novolis-machinelearning).

## Packages

| Package | Purpose |
|---|---|
| `Novolis.Inference.Abstractions` | Provider-neutral chat contracts |
| `Novolis.Inference.Ollama` | Ollama local chat |
| `Novolis.Inference.SharpMind` | SharpMind local model adapter |
| `Novolis.Inference.AgentFramework` | Microsoft Agent Framework / `IChatClient` adapter |
| `Novolis.Inference.Images` | Image generation contracts |
| `Novolis.Inference.Onnx` | ONNX Runtime image graph adapter |
| `Novolis.Inference.Safety` | Image safety policies and classifiers |

The old Semantic Kernel playground is not a platform boundary. Provider and framework integrations are rewritten against current APIs here.

See [getting started](docs/getting-started.md), [design](docs/design.md), and [release](docs/release.md).
