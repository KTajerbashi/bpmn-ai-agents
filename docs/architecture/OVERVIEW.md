# Architecture Overview

## Architectural style

The target system uses independently deployable services with Clean Architecture inside each service. Microservices are a deployment and ownership choice; Clean Architecture is an internal dependency rule. They solve different problems and should not be conflated.

## Logical components

- **Gateway:** routing, edge concerns, and request correlation. It is not a place for domain logic.
- **Identity:** integrates with an OIDC provider and owns platform-specific authorization data only when needed.
- **Process Catalog:** stores BPMN XML and immutable versions, validates definitions, and manages publication metadata.
- **Workflow:** coordinates process lifecycle and isolates the chosen BPMN runtime behind `IWorkflowEngine`.
- **Task Management:** owns user-task state, claims, assignments, due dates, and completion.
- **Agent Orchestrator:** owns agent definitions/versions, execution plans, run state, and coordination.
- **Agent Workers:** perform durable background work and invoke provider/tool adapters.
- **Integration:** contains adapters for external enterprise systems.
- **Audit:** centralizes audit records only when the requirements justify a dedicated service; otherwise begin with service-local audit events and evolve later.

## Dependency direction within a service

```text
Api / Worker Host
       |
       v
Infrastructure ---> Application ---> Domain
       |                 |
       +-----------------+  (implements application ports)
```

The diagram represents compile-time references: Application must not reference Infrastructure. Domain remains the innermost layer. Infrastructure references Application to implement its interfaces; the host composes all implementations through dependency injection.

## Persistence ownership

Recommended logical ownership:
- `BpmnIdentityDb`
- `BpmnCatalogDb`
- `BpmnWorkflowDb`
- `BpmnTasksDb`
- `BpmnAgentsDb`
- `BpmnIntegrationDb`
- `BpmnAuditDb` (only if a dedicated audit service is warranted)

A single SQL Server instance may host these databases initially. Database-per-service means ownership and schema isolation, not necessarily a separate SQL Server instance for every service. The external BPMN engine may require its own supported persistence store; do not modify its internal tables directly.

## Workflow runtime boundary

The Application layer depends on a port such as `IWorkflowEngine`. An adapter translates platform commands and events to the chosen engine's API/job mechanism. Do not leak engine SDK types into domain models or public service contracts.

The adapter contract must cover at least:
- Deploy or reference a published process version.
- Start an instance with validated input and correlation metadata.
- Query status and relevant execution metadata.
- Cancel an instance when supported.
- Correlate a job result or message back into a running instance.
- Map engine failures to platform-level error categories.
- Expose health/readiness and support recovery/reconciliation.

Exact methods depend on the engine selected by the ADR.

## AI boundary

`IAiModelProvider` abstracts model invocation. `IAgentExecutor` executes a versioned agent definition, while `IAgentTool` represents a specific, allowlisted capability. The orchestrator owns policy, execution state, and coordination; a model does not own application authorization or workflow state.

## Multi-agent patterns

Implement only the patterns required by real use cases:
- Sequential pipeline.
- Parallel fan-out with bounded concurrency and a join policy.
- Supervisor/delegation with explicit tool and step limits.
- Specialist agents followed by a deterministic aggregator/validator.

Every run needs a maximum step count, timeout, cost/token budget, cancellation policy, and defined behavior for partial failure.

## Communication

- Synchronous HTTP/gRPC only where a response is required to proceed immediately.
- RabbitMQ for durable asynchronous work and integration events.
- Versioned contracts, correlation IDs, idempotency keys, and explicit retry/DLQ behavior.
- Avoid synchronous chains across many services.

## Key reliability pattern

A database transaction cannot atomically commit both local SQL state and a publish to RabbitMQ. Use an Outbox: persist state and the outgoing event in one local transaction, then publish from a dispatcher. Consumers record message identity or use idempotent state transitions. For external engine calls, persist an operation state and reconcile uncertain outcomes rather than assuming a distributed transaction.
