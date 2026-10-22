# Security and Observability

## Security controls

- Integrate with an OAuth 2.0 / OpenID Connect identity provider.
- Enforce authorization in application use cases, not only in the UI or gateway.
- Derive tenant scope from trusted identity/context; never trust a tenant ID supplied by an AI model.
- Store secrets in an approved secret store or deployment secret mechanism.
- Use least-privilege credentials for databases, brokers, providers, and tools.
- Allowlist tools and validate each tool's arguments and authorization independently.
- Apply request limits, rate limits, timeouts, concurrency caps, and AI budget limits.
- Require human confirmation for sensitive or irreversible actions.
- Treat prompt injection and retrieved content as untrusted input.
- Minimize sensitive payload retention and define deletion/retention policies.
- Do not log access tokens, API keys, full secrets, or unnecessary personal data.

## Audit fields

Where appropriate, record:
- Actor or service identity.
- Tenant and correlation IDs.
- Process definition/version and process instance.
- Agent definition/version and execution ID.
- Model/provider identifier and relevant configuration version.
- Tool invoked, authorization outcome, and result category.
- Approval identity and decision.
- Timestamps and final outcome.

Do not assume all prompts and outputs should be stored forever. Define data classification and retention first.

## Observability

Use OpenTelemetry-compatible instrumentation for:
- Distributed traces across HTTP, broker consumers, workers, and external calls.
- Structured logs with stable identifiers.
- Metrics for queue depth, oldest message age, execution duration, retries, failures, model latency/cost, and workflow incidents.
- Health checks for process liveness and dependency readiness.

Avoid high-cardinality metric labels such as raw process instance IDs or user IDs. Put those identifiers in traces/logs instead.

## Operational runbooks

Create runbooks for:
- Queue backlog and stuck consumers.
- Dead-letter inspection and replay.
- Workflow instance stuck in an incident.
- Model provider outage or rate limiting.
- Outbox backlog.
- Database restore and recovery.
- Suspected unauthorized tool execution.
