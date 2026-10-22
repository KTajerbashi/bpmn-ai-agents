# ADR 0003: Reliable Asynchronous Messaging

- Status: Accepted
- Date: TBD

## Context

Long-running AI jobs and cross-service state changes cannot safely depend on synchronous request chains or an assumed atomic transaction between SQL Server and RabbitMQ.

## Decision

Use RabbitMQ for asynchronous cross-service integration. Use an Outbox for publishing events after local database state changes where consistency requires it. Consumers must be idempotent. Assume at-least-once delivery and implement bounded retry and dead-letter handling.

## Consequences

- Event contracts need explicit versions and ownership.
- Outbox dispatchers and consumer deduplication require operational metrics.
- Eventual consistency must be visible in API states and UI.
- Exactly-once business effects must be achieved through idempotent application behavior, not assumed broker guarantees.

## Revisit triggers

Revisit broker technology if throughput, managed-service constraints, or operational requirements materially change.
