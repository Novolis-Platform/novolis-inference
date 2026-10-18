# Design

Machine learning builds and trains models. Inference loads, calls, and evaluates them.

The package direction is:

```text
provider-neutral contracts
  → provider adapters (Ollama, SharpMind, ONNX)
  → framework adapters (Microsoft Agent Framework)
  → product hosts and labs
```

The Agent Framework package is an integration seam. It keeps Microsoft framework API changes in one package while `IChatModel` remains stable for Novolis consumers.
