# BPMN Engine Evaluation

## Decision status

**Not selected yet.** Evaluate Camunda 8 and Elsa against representative processes before committing to an engine. Do not choose based only on a feature checklist or a sample hello-world.

`bpmn.io` is a BPMN modeling/editor toolkit, not a process execution engine.

## Required proof-of-concept scenarios

1. Import and validate BPMN XML produced by the intended designer.
2. Execute start/end events, service tasks, gateways, timers, and message correlation required by the product.
3. Pause for a human task and resume after completion.
4. Invoke an asynchronous AI job and correlate its result back to the workflow.
5. Demonstrate retries, incident visibility, cancellation, and recovery after worker restart.
6. Deploy and execute immutable process versions.
7. Apply authentication and tenant isolation.
8. Observe execution through logs, metrics, and traces.
9. Operate locally and in the intended production environment.
10. Estimate licensing, hosting, operational effort, and upgrade risk.

## Evaluation dimensions

Score each candidate from 1 (poor) to 5 (excellent) and record evidence, caveats, and version tested.

| Dimension | Weight | Evidence to collect |
|---|---:|---|
| BPMN compatibility | 20% | Actual diagrams from product requirements |
| Runtime reliability and recovery | 15% | Failure/restart tests |
| Human tasks and correlation | 10% | End-to-end scenarios |
| .NET integration ergonomics | 10% | Worker/adapter complexity |
| Operational model | 10% | Deployment, upgrades, monitoring |
| Versioning and deployment | 10% | Immutable version tests |
| Security / multi-tenancy | 10% | Authorization and tenant test |
| Cost and licensing | 10% | Verified current terms and estimate |
| Developer experience | 5% | Local setup and debugging |

Weights may be adjusted to fit product requirements. Record links and dates for vendor-specific claims.

## Architecture guardrail

Use a platform-owned `IWorkflowEngine` abstraction only for capabilities the application actually needs. Do not attempt to create a lowest-common-denominator abstraction for every BPMN feature. Keep engine-specific capabilities explicit where they cannot be portably represented.

## Decision output

Create an ADR that documents:
- Requirements and representative BPMN files.
- Engine versions and environment tested.
- Results and unsupported features.
- Operational and commercial constraints.
- Selected engine and rationale.
- Migration/exit considerations.
- Revisit triggers.
