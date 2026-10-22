# Development Roadmap

The roadmap is milestone-based. Each milestone should deliver a testable vertical slice. Avoid creating every planned service as an empty project.

## Milestone 0 — Requirements and decisions

**Deliverables**
- Define initial user journeys and BPMN constructs.
- Capture non-functional requirements: tenancy, security, retention, throughput, latency, availability, recovery.
- Create ADR for workflow engine evaluation.
- Agree on event naming/versioning and API error conventions.

**Acceptance criteria**
- Three to five representative BPMN scenarios exist as test assets.
- Engine evaluation criteria and decision owners are documented.
- Open architectural questions are visible and prioritized.

## Milestone 1 — Repository foundation

**Deliverables**
- Solution structure and first runnable API/worker.
- Pin SDK and package versions.
- `.editorconfig`, nullable reference types, analyzers, formatting.
- CI workflow for restore, build, test, and format verification.
- Structured logging, health checks, correlation ID propagation.
- Local Docker Compose dependencies.

**Acceptance criteria**
- A clean clone builds using documented commands.
- CI fails on build/test/format errors.
- Secrets are not stored in source control.

## Milestone 2 — Process Catalog

**Deliverables**
- Process definition and immutable version lifecycle.
- BPMN XML storage and content hashing.
- BPMN validation adapter.
- Draft, validate, publish, and retrieve use cases.
- Authorization and audit for publication.

**Acceptance criteria**
- Invalid XML or unsupported required constructs are rejected with actionable errors.
- Published versions cannot be mutated.
- Unit and integration tests cover versioning and authorization.

## Milestone 3 — Workflow engine proof of concept

**Deliverables**
- Compare Camunda 8 and Elsa using the same representative scenarios.
- Implement a minimal adapter for the leading candidate.
- Validate start, status, human wait/resume, async job, failure, and recovery.
- Complete ADR 0001.

**Acceptance criteria**
- Results include tested versions and evidence.
- Unsupported features and operational constraints are explicit.
- The engine is selected by documented decision, not assumption.

## Milestone 4 — Workflow service

**Deliverables**
- Start/query/cancel process instance use cases.
- Runtime adapter and platform execution records.
- Correlation IDs and execution history.
- Reconciliation for uncertain runtime operations.

**Acceptance criteria**
- API can start a published version and query lifecycle state.
- Duplicate start requests are handled through idempotency policy.
- Engine outage and timeout behavior are tested.

## Milestone 5 — Messaging reliability

**Deliverables**
- RabbitMQ topology and versioned event contracts.
- Outbox dispatcher.
- Idempotent consumer/inbox where required.
- Retry, dead-letter, replay and operational metrics.

**Acceptance criteria**
- Simulated duplicate delivery does not duplicate business effects.
- Crash between database commit and publish is recovered.
- Poison messages are visible and do not loop indefinitely.

## Milestone 6 — Human tasks

**Deliverables**
- Task creation, claim, assign/reassign, complete/reject.
- Candidate users/groups and due dates as required.
- Authorization and task audit.
- Workflow correlation/resume.

**Acceptance criteria**
- Only authorized users can claim or complete a task.
- Concurrent claims are handled safely.
- Completing a task resumes the correct workflow activity.

## Milestone 7 — Single AI agent

**Deliverables**
- `IAiModelProvider`, `IAgentExecutor`, and `IAgentTool` boundaries.
- Versioned agent configuration and structured output schema.
- Worker queue and execution status endpoints.
- Tool allowlist, budgets, timeout, cancellation and audit.
- AI evaluation fixtures.

**Acceptance criteria**
- AI execution is asynchronous and queryable by ID.
- Invalid model output is rejected safely.
- Tool authorization is enforced outside the model.
- Provider errors and rate limits follow bounded retry rules.

## Milestone 8 — BPMN AI activity integration

**Deliverables**
- AI task request contract.
- Workflow-to-orchestrator dispatch.
- Result/failure correlation back to the runtime.
- Recovery for late, duplicate, or unknown results.

**Acceptance criteria**
- An end-to-end BPMN process completes through an AI activity.
- Duplicate result events do not advance the workflow twice.
- AI failure and timeout produce documented workflow states.

## Milestone 9 — Multi-agent orchestration

**Deliverables**
- One initial pattern, preferably sequential or bounded parallel.
- Run group, step tracking, aggregation/validation.
- Global step, concurrency, timeout, and cost limits.
- Partial-failure and cancellation policy.

**Acceptance criteria**
- Runs are inspectable at group and step level.
- Failure of one participant follows an explicit policy.
- The orchestrator prevents unbounded loops and uncontrolled fan-out.

## Milestone 10 — Security and tenancy

**Deliverables**
- OIDC integration and role/permission policy.
- Tenant isolation tests.
- Secret management and credential rotation approach.
- Sensitive-action approval policy.
- Retention and data classification rules.

**Acceptance criteria**
- Cross-tenant access tests fail safely.
- Authorization applies to APIs, consumers, and workers.
- Logs and stored payloads follow data-minimization rules.

## Milestone 11 — Operations and production readiness

**Deliverables**
- Traces, metrics, dashboards and alerts.
- Queue/outbox/DLQ runbooks.
- Backup and restore verification.
- Load, resilience, and security tests.
- Deployment automation and rollback strategy.

**Acceptance criteria**
- Operators can identify stuck workflow and AI executions.
- Restore procedure is tested, not merely documented.
- SLOs, capacity assumptions, and alert thresholds are approved.

## Suggested priority order

1. Requirements + engine proof of concept.
2. Repository foundation.
3. Process Catalog.
4. Workflow service.
5. Messaging reliability.
6. One human task path.
7. One AI agent.
8. AI activity end-to-end.
9. Multi-agent execution.
10. Security hardening and operations.

Some foundation and proof-of-concept tasks can proceed in parallel, but the engine decision must be evidence-based.

## Definition of Done

A milestone is done only when:
- Acceptance criteria pass.
- Automated tests cover normal and relevant failure paths.
- Authorization and input validation are addressed.
- Logs/metrics/correlation are adequate to troubleshoot.
- API/event contracts and docs are updated.
- Configuration and secrets are externalized.
- Migration, deployment, rollback, and recovery implications are documented.
- No unverified claim is made that the build or tests passed.
