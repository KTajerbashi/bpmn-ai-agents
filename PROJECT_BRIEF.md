# Project Brief — BPMN AI Agents

## Purpose

Build a .NET platform that executes versioned BPMN workflows and enables workflows to call AI agents, coordinate multiple agents, request human approval, and integrate with enterprise systems.

## Product principles

1. **Workflow first:** BPMN remains the explicit orchestration model for business processes.
2. **AI is a bounded activity:** AI is invoked through controlled contracts, not allowed to own the workflow lifecycle.
3. **Replaceable infrastructure:** workflow engines, model providers, brokers, and storage implementations are accessed through adapters where useful.
4. **Reliable by design:** durable state, idempotency, retries, recovery, and observability are first-class.
5. **Secure by default:** least privilege, tool allowlists, tenant isolation, auditability, and human approval for sensitive operations.
6. **Incremental delivery:** ship vertical slices rather than creating many empty microservices.

## First release success criteria

- Create, validate, version, and publish a BPMN process.
- Start a process instance and inspect its status/history.
- Execute one AI activity asynchronously and resume the process with validated output.
- Support one human task lifecycle.
- Demonstrate retry and recovery after a worker or broker failure.
- Correlate a request, process instance, AI execution, and resulting event.
- Run the core stack locally using documented commands.
- Pass unit, integration, contract, and end-to-end tests for the primary path.

## Decisions not yet finalized

- BPMN runtime: Camunda 8 or Elsa, selected through a proof of concept.
- Public API versioning and error format.
- Tenant model and identity provider.
- AI model providers and tool execution environment.
- Deployment platform and production SLOs.
- Exact microservice split after validating domain boundaries.
