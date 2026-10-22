# Messaging and Reliability

## Event envelope

A cross-service event should carry a stable envelope similar to:

```json
{
  "eventId": "uuid",
  "eventType": "AgentExecutionCompleted",
  "schemaVersion": 1,
  "occurredAt": "2026-01-01T12:00:00Z",
  "correlationId": "correlation-id",
  "causationId": "optional-parent-event-or-command-id",
  "tenantId": "optional-tenant-id",
  "payload": {}
}
```

Use a shared envelope only for genuinely shared metadata. Avoid a giant shared contract library that couples every service to every domain. Version event payloads deliberately and maintain compatibility rules.

## Candidate events

- `ProcessVersionPublished`
- `ProcessInstanceStarted`
- `AiTaskRequested`
- `AgentExecutionStarted`
- `AgentExecutionCompleted`
- `AgentExecutionFailed`
- `HumanTaskCompleted`
- `WorkflowInstanceCompleted`
- `WorkflowInstanceFailed`

Event names and payloads must be finalized alongside the owning use cases.

## Delivery semantics

Assume at-least-once delivery. Exactly-once business effects are not guaranteed by a broker. Consumers must be idempotent.

Recommended techniques:
- Unique event/message ID.
- Consumer inbox or processed-message table for deduplication where necessary.
- Idempotent state transitions and unique constraints.
- Outbox for publishing events after local state changes.
- Bounded retries with exponential backoff and jitter.
- Dead-letter queue with documented inspection/replay process.
- Correlation and causation IDs in logs and persisted execution records.

## Outbox flow

1. Begin a local database transaction.
2. Apply the business state transition.
3. Insert the outgoing event in the Outbox table.
4. Commit.
5. A background dispatcher publishes pending events.
6. Mark successful publication; tolerate a crash between publish and mark by making consumers idempotent.

The dispatcher must handle poison events, backpressure, shutdown, and operational metrics.

## Workflow-engine calls

A remote engine call cannot be committed atomically with a local SQL transaction by assumption. Use an operation state machine, idempotency/correlation where supported, and reconciliation for uncertain outcomes. Do not automatically repeat a non-idempotent start call after a timeout without checking whether the first call succeeded.

## Retry policy

Classify failures:
- Transient infrastructure failure: bounded retry with backoff.
- Rate limit: honor provider retry guidance where available.
- Invalid input or schema: fail fast and record a useful error.
- Policy/authorization rejection: do not retry automatically.
- Model quality failure: use a bounded repair/retry policy with a maximum budget.
- Poison event: dead-letter and alert; replay only with an operator procedure.

Retries need an overall deadline and must not multiply uncontrolled across layers.
