# BPMN AI Agents

**An extensible .NET platform for executing BPMN workflows with AI agents, human tasks, and reliable service integrations.**

> Project status: Architecture and planning  
> Primary stack: .NET, ASP.NET Core, Clean Architecture, MediatR, EF Core, SQL Server, RabbitMQ  
> Workflow engine: To be selected after a focused proof of concept (Camunda 8 or Elsa)

## Vision

BPMN AI Agents connects visual business-process modeling with reliable workflow execution and AI-powered work. A process can combine deterministic BPMN activities, human approval, external integrations, a single AI agent, or coordinated multi-agent execution.

The platform is designed as independently deployable services with explicit domain ownership, asynchronous messaging, observable execution, and replaceable infrastructure adapters.

## Goals

- Execute versioned BPMN process definitions.
- Support single-agent AI tasks and multi-agent collaboration patterns.
- Combine AI activities with human tasks, timers, gateways, and integrations.
- Keep workflow execution independent from AI-provider implementations.
- Use Clean Architecture inside each service.
- Use MediatR for in-process application use cases, not as a cross-service transport.
- Use asynchronous messaging for long-running work and cross-service integration.
- Provide traceability, audit history, retries, idempotency, and operational visibility.
- Enforce tenant boundaries, authorization, tool allowlists, and human approval where required.

## Non-goals for the first release

- Building a new BPMN execution engine from scratch.
- Allowing AI-generated code or unrestricted tools to execute in production.
- Requiring Kubernetes before local development and deployment workflows are mature.
- Sharing EF Core entities or a single DbContext across services.

## Proposed technology baseline

| Concern | Initial choice |
|---|---|
| Runtime | Current supported .NET LTS release, pinned in `global.json` |
| API | ASP.NET Core Web API |
| Application use cases | MediatR |
| Relational persistence | EF Core + SQL Server |
| Cross-service messaging | RabbitMQ |
| API authentication | OAuth 2.0 / OpenID Connect |
| Telemetry | OpenTelemetry |
| Local environment | Docker Compose |
| BPMN engine | Select through a documented proof of concept |
| Frontend / BPMN editor | Separate frontend; bpmn.io may be used as a designer, not as an execution engine |

Pin exact package versions and the .NET SDK in the repository when implementation begins. Avoid floating dependency versions in production builds.

## Architecture at a glance

```text
Client / BPMN Designer
        |
   API Gateway / Identity
        |
  +-----+--------------------+
  |                          |
Process Catalog         Workflow Service
  |                          |
Versioned BPMN XML       IWorkflowEngine
                             |
                    BPMN Runtime Adapter
                    (Camunda or Elsa)
                             |
                     AI task / User task
                             |
                 +-----------+-----------+
                 |                       |
          Agent Orchestrator       Task Management
                 |
          Agent Worker(s)
                 |
       AI Model Provider + Allowlisted Tools

Cross-service communication: RabbitMQ
Service-owned data: SQL Server databases/schemas
Telemetry: OpenTelemetry, structured logs, metrics, traces
```

The diagram is logical, not a mandate to deploy every component as a separate service on day one. Keep service boundaries explicit, but split and deploy a component independently when there is a clear operational or ownership reason.

## Repository layout

```text
bpmn-ai-agents/
├── src/
│   ├── Gateway/BpmnAiAgents.Gateway/
│   ├── Services/
│   │   ├── Identity/{Api,Application,Domain,Infrastructure}/
│   │   ├── ProcessCatalog/{Api,Application,Domain,Infrastructure}/
│   │   ├── Workflow/{Api,Application,Domain,Infrastructure}/
│   │   ├── TaskManagement/{Api,Application,Domain,Infrastructure}/
│   │   ├── AgentOrchestrator/{Api,Application,Domain,Infrastructure}/
│   │   ├── AgentWorkers/{Host,Application,Infrastructure}/
│   │   ├── Integration/{Api,Application,Domain,Infrastructure}/
│   │   └── Audit/{Api,Application,Domain,Infrastructure}/
│   └── BuildingBlocks/
│       ├── Contracts/
│       ├── Messaging/
│       ├── Observability/
│       ├── Security.Abstractions/
│       └── Testing/
├── tests/
│   ├── UnitTests/
│   ├── IntegrationTests/
│   ├── ContractTests/
│   ├── ArchitectureTests/
│   └── EndToEndTests/
├── deploy/
│   ├── docker/
│   ├── compose/
│   ├── kubernetes/
│   └── observability/
├── docs/
│   ├── architecture/
│   ├── adr/
│   └── development/
├── .github/workflows/
├── .editorconfig
├── Directory.Build.props
├── Directory.Packages.props
├── global.json
└── README.md
```

This is a target structure. Create projects incrementally; do not generate empty projects for every planned service before their boundaries and use cases are justified.

## Core service responsibilities

| Service | Owns |
|---|---|
| Process Catalog | BPMN definitions, immutable versions, validation and publication metadata |
| Workflow | Process start/cancel/query, runtime adapter, execution coordination and recovery |
| Task Management | Human tasks, claims, assignments, completion and due dates |
| Agent Orchestrator | Agent definitions, execution plans, run state, coordination and result validation |
| Agent Workers | Background execution of agent jobs and tool calls |
| Integration | External system adapters and outbound integration policies |
| Identity | Authentication integration, users/roles/permissions where owned by the platform |
| Audit | Append-oriented audit records and operational history where centralization is justified |

Each service owns its persistence model. A single SQL Server instance may host multiple service databases during early development, but one service must not write directly to another service's tables.

## Clean Architecture rules

- `Domain` contains business rules and must not depend on EF Core, ASP.NET Core, MediatR, brokers, or vendor SDKs.
- `Application` defines use cases, ports, validation, and transaction boundaries.
- `Infrastructure` implements persistence, messaging, model providers, and external adapters.
- `Api` or `Host` is the composition root and delivery mechanism.
- Dependencies point inward: Infrastructure and API depend on Application; Application depends on Domain.
- Use MediatR for in-process commands and queries. Use versioned contracts and messaging for cross-service communication.
- Do not share domain entities or `DbContext` instances across services.
- Use `CancellationToken` for I/O and long-running operations.
- Make handlers small and keep business rules in domain objects or explicit domain/application services.

## Workflow and AI execution model

1. A published immutable process version is selected.
2. The Workflow service starts an instance through `IWorkflowEngine`.
3. The BPMN runtime reaches an AI activity and emits or activates a job.
4. The Agent Orchestrator creates an execution record and queues work.
5. A worker executes a single agent or a multi-agent plan.
6. Output is validated against a schema and business rules; sensitive actions may require approval.
7. A result event is published reliably and consumed idempotently.
8. The workflow runtime receives the result and continues to the next BPMN activity.

Long-running AI work must not be tied to an open HTTP request. Return an execution identifier and an accepted/queued response, then expose status and result endpoints.

## Initial API surface (illustrative)

### Process Catalog
- `POST /api/process-definitions`
- `POST /api/process-definitions/{id}/versions`
- `POST /api/process-versions/{id}/validate`
- `POST /api/process-versions/{id}/publish`
- `GET /api/process-definitions/{id}`
- `GET /api/process-definitions/{id}/versions`

### Workflow
- `POST /api/process-instances`
- `GET /api/process-instances/{id}`
- `POST /api/process-instances/{id}/cancel`
- `GET /api/process-instances/{id}/history`

### Task Management
- `GET /api/tasks/my`
- `GET /api/tasks/{id}`
- `POST /api/tasks/{id}/claim`
- `POST /api/tasks/{id}/complete`
- `POST /api/tasks/{id}/reject`
- `POST /api/tasks/{id}/reassign`

### Agent Orchestrator
- `POST /api/agent-executions`
- `GET /api/agent-executions/{id}`
- `POST /api/agent-runs`
- `GET /api/agent-runs/{id}`

Endpoints are proposals, not a stable public contract. Add authorization, pagination, idempotency, error conventions, and API versioning before production release.

## Reliability requirements

- Use an Outbox pattern when database state changes must be published as events.
- Consumers must be idempotent and tolerate duplicate delivery.
- Define bounded retries, exponential backoff, dead-letter handling, and a replay procedure.
- Model external engine operations as recoverable state transitions; do not assume SQL Server and a remote workflow engine share an atomic transaction.
- Store correlation and causation identifiers across HTTP requests, jobs, and events.
- Apply timeouts and cancellation to external calls.
- Persist execution state before acknowledging work.
- Treat AI output as untrusted input and validate it before changing business state.

## Security baseline

- OAuth 2.0 / OpenID Connect for identity integration.
- Authorization checks at API and use-case boundaries.
- Tenant scoping enforced in queries and background jobs.
- Secrets stored outside source control.
- Explicit tool allowlists and per-tool authorization.
- Rate limits, model budgets, request size limits, and execution timeouts.
- Human approval for consequential or irreversible actions.
- Audit actor, process version, agent version, model/provider, tool calls, decisions, and outcome where appropriate.
- Never treat model output as authorization.

## Local development

The local development environment will be defined in `deploy/compose/`. It should provide the dependencies required by the currently implemented services, such as SQL Server and RabbitMQ, plus health checks and persistent development volumes.

Typical setup after the first implementation milestone:

```bash
git clone <your-repository-url>
cd bpmn-ai-agents
docker compose -f deploy/compose/docker-compose.yml up -d
dotnet restore
dotnet build
dotnet test
```

The compose file and exact commands will be added with the first runnable vertical slice. Do not assume this command works before that milestone lands.

## Testing strategy

- Unit tests: domain rules and application use cases.
- Integration tests: database mappings, broker behavior, and external adapters.
- Contract tests: event and API compatibility between services.
- Architecture tests: enforce dependency direction and prevent unwanted references.
- Workflow tests: BPMN validation, runtime adapter behavior, retries, and recovery.
- AI evaluation tests: structured-output validity, tool policy, quality thresholds, and regression cases.
- End-to-end tests: a process that includes an AI task and a human or integration task.

## Roadmap

1. **Foundation** — solution, SDK/package pinning, coding standards, CI, logging, health checks.
2. **Process Catalog** — BPMN XML storage, immutable versions, validation, publish workflow.
3. **Workflow engine proof of concept** — compare Camunda 8 and Elsa against real process scenarios; document the decision in an ADR.
4. **Workflow service** — start/query/cancel, runtime adapter, history and recovery.
5. **Messaging reliability** — RabbitMQ, contracts, Outbox, idempotent consumers, retry/DLQ.
6. **Human tasks** — task lifecycle, claims, assignment, completion and authorization.
7. **Single AI agent** — provider abstraction, structured output, tool allowlist, budgets and audit.
8. **AI task integration** — BPMN AI activity to queued agent execution and workflow resumption.
9. **Multi-agent orchestration** — sequential, parallel, and supervisor/aggregator patterns with limits.
10. **Security and tenant isolation** — end-to-end authorization, secrets, audit and abuse controls.
11. **Operations** — dashboards, traces, metrics, alerts, backup/restore and load tests.
12. **Production hardening** — failure drills, security review, performance targets and deployment automation.

See [`docs/development/ROADMAP.md`](docs/development/ROADMAP.md) for acceptance criteria.

## Architecture decisions

Important decisions are recorded in `docs/adr/`. At minimum, record the workflow engine choice, service boundaries, messaging semantics, persistence ownership, AI-provider abstraction, and deployment strategy. Each ADR should include context, options, decision, consequences, and a revisit trigger.

## Contributing

1. Open or update an issue describing the use case and acceptance criteria.
2. Keep changes scoped to one service or a documented cross-service contract.
3. Add tests for business rules and failure behavior.
4. Update architecture documentation and ADRs when a boundary or contract changes.
5. Ensure formatting, build, tests, and architecture checks pass.

## License

Choose and add a license before publishing the repository publicly.
