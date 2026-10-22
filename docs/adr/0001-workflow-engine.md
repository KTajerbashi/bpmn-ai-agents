# ADR 0001: Select the BPMN Workflow Runtime

- Status: Proposed
- Date: TBD
- Decision owners: Project maintainers

## Context

The platform must execute BPMN processes that include deterministic tasks, gateways, timers, human work, external integrations, and asynchronous AI activities. The .NET application should not depend directly on one runtime throughout its domain and application layers.

## Options

1. Camunda 8 with .NET workers/adapters.
2. Elsa Workflows, after verifying required BPMN import/execution compatibility for the targeted version.
3. A different runtime if requirements or proof-of-concept evidence justify it.
4. Build a custom runtime only if existing engines cannot meet critical requirements and the cost is explicitly accepted.

## Decision

Pending a proof of concept. Do not mark this ADR Accepted until representative scenarios pass and licensing/operational requirements are reviewed.

## Consequences

- Application code will depend on a small `IWorkflowEngine` port.
- Runtime-specific types remain in the adapter project.
- Engine capability gaps must be documented rather than hidden.
- The engine owns its internal persistence requirements; the platform will not write directly to engine tables.

## Revisit triggers

Revisit if required BPMN constructs are unsupported, operational costs change materially, the runtime is no longer maintained, or portability requirements change.
