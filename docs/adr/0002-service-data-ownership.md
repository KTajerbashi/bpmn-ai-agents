# ADR 0002: Service Data Ownership

- Status: Accepted
- Date: TBD

## Context

The platform has separate business capabilities for process catalog, workflow execution, human tasks, and AI orchestration. Shared database tables would couple their release cycles and persistence models.

## Decision

Each service owns its schema and data lifecycle. Other services access that data through versioned APIs or integration events, not direct database writes. A shared SQL Server instance is acceptable in early environments if ownership remains isolated.

## Consequences

- No shared `DbContext` or cross-service EF entities.
- Cross-service queries may require read models or explicit API calls.
- Data duplication for read/query needs is acceptable when consistency expectations are documented.
- External workflow engine tables are managed only by the engine.

## Revisit triggers

Revisit the physical database topology when operational isolation, scaling, compliance, or ownership requires it. Do not revisit logical ownership merely to simplify a query.
