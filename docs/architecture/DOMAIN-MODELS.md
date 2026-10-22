# Domain Model Sketch

This document proposes initial concepts. It is not a command to create all entities before the corresponding use cases exist.

## Process Catalog

### ProcessDefinition
Represents a logical process identity.
- `Id`
- `Key`
- `Name`
- `Description`
- `TenantId` (if multi-tenancy is required)
- `CreatedAt`
- `CreatedBy`

### ProcessVersion
An immutable version of BPMN XML and associated metadata.
- `Id`
- `ProcessDefinitionId`
- `Version`
- `BpmnXml` or a reference to immutable object storage
- `ContentHash`
- `ValidationStatus`
- `CreatedAt`
- `CreatedBy`

Published versions must not be edited in place. Publish a new version instead.

### ProcessDeployment
Tracks deployment/publication to a specific runtime or environment.
- `Id`
- `ProcessVersionId`
- `Environment`
- `RuntimeDeploymentId`
- `Status`
- `DeployedAt`

## Workflow

### ProcessInstance
Platform-level identity and lifecycle for a process execution.
- `Id`
- `ProcessVersionId`
- `RuntimeInstanceId`
- `Status`
- `CorrelationId`
- `StartedAt`
- `CompletedAt`
- `FailureCode`

Do not duplicate every runtime engine field unless the platform needs it for querying, authorization, audit, or recovery.

### WorkflowExecutionRecord
Append-oriented lifecycle/history record:
- `Id`
- `ProcessInstanceId`
- `ActivityKey`
- `EventType`
- `OccurredAt`
- `CorrelationId`
- `PayloadReference` (avoid storing sensitive payloads unnecessarily)

### WorkflowIncident
Represents a failure requiring retry, remediation, or operator attention:
- `Id`
- `ProcessInstanceId`
- `Code`
- `Message`
- `Status`
- `CreatedAt`
- `ResolvedAt`

## Task Management

### WorkflowTask
- `Id`
- `ProcessInstanceId`
- `TaskKey`
- `Title`
- `Status`
- `AssigneeId`
- `CandidateGroup`
- `DueAt`
- `CreatedAt`
- `CompletedAt`

### TaskAssignment
Optional history for claim, assignment, reassignment, and release actions.

## Agent Orchestrator

### AgentDefinition / AgentVersion
A logical agent and immutable executable configuration/version. Store prompt templates, output schemas, permitted tools, model policy, and budget policy as versioned configuration. Keep secrets outside the definition.

### AgentExecution
- `Id`
- `AgentVersionId`
- `Status`
- `InputReference` or validated input
- `OutputReference` or validated output
- `CorrelationId`
- `StartedAt`
- `CompletedAt`
- `FailureCode`

### AgentRunGroup
Coordinates a multi-agent run with a defined plan, limits, and terminal policy.

### AgentStep
Records an individual delegated step or agent invocation, its input/output references, status, and parent run.

## Modeling guidance

- Use strongly typed identifiers where they materially improve correctness; do not over-engineer every primitive.
- Keep persistence entities separate from public API contracts where the shapes or lifecycles differ.
- Prefer explicit status transitions over arbitrary string updates.
- Record timestamps in UTC.
- Decide retention and privacy rules before persisting prompts, outputs, or tool payloads.
- Add optimistic concurrency for state that may be updated by multiple workers.
