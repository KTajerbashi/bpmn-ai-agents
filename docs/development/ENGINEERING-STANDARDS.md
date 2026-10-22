# Engineering Standards

## C# and project conventions

- Pin the SDK and centralize package versions.
- Enable nullable reference types.
- Use async APIs and propagate `CancellationToken`.
- Avoid `.Result`, `.Wait()`, and sync-over-async.
- Keep controllers and endpoint handlers thin.
- Validate requests at the application boundary.
- Return consistent problem details and stable error codes.
- Use UTC timestamps and explicit time-zone conversion at presentation boundaries.
- Use `AsNoTracking` for read-only EF Core queries when appropriate.
- Keep database migrations owned by the service that owns the database.

## Application design

- A command changes state; a query reads state.
- MediatR is an in-process dispatch mechanism, not a distributed messaging mechanism.
- Use domain methods for invariants and explicit application services for use-case orchestration.
- Keep transaction boundaries local to a service.
- Avoid generic repository abstractions that only duplicate EF Core without adding a useful boundary.
- Do not put business logic in controllers, EF Core configuration, or message consumers.

## API design

- Define request/response DTOs independently from persistence entities.
- Use pagination for collection endpoints.
- Define idempotency behavior for retryable commands.
- Return an execution resource/ID for long-running work.
- Document authorization, status transitions, and error cases.
- Version public APIs and event schemas intentionally.

## Testing

- Unit tests for domain invariants and application logic.
- Integration tests for SQL mappings and infrastructure adapters.
- Contract tests for APIs and events.
- Architecture tests for dependency rules.
- End-to-end tests for the critical BPMN + AI path.
- Failure tests for duplicate messages, timeouts, worker restarts, provider rate limits, and uncertain engine operations.

## Pull requests

Every PR should state:
- Problem and expected behavior.
- Owning service and affected contracts.
- Data migration or event-version implications.
- Security and failure-path considerations.
- Tests run and actual results.
- Documentation/ADR updates.
