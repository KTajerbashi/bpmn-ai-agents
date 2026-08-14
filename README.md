# .NET Engineering Lab

A practical **.NET and C# engineering reference repository** containing isolated, runnable examples of technologies, patterns, infrastructure components, development techniques, and architectural concepts used in modern software systems.

This repository is designed as a **personal engineering laboratory and reusable reference** rather than a single business application.

The goal is simple:

> Learn a concept → build a small working example → document it → keep it available for future projects.

---

## 🎯 Purpose

The purpose of this repository is to provide a centralized collection of practical .NET examples that can be used for:

* Learning and experimenting with .NET technologies
* Understanding software engineering concepts through working code
* Evaluating libraries and infrastructure components
* Prototyping solutions before introducing them into production systems
* Keeping reusable technical references for future projects
* Comparing different implementation approaches
* Testing architectural and design patterns in isolation

Each sample focuses on a specific concept instead of trying to implement a complete business domain.

---

## 🧭 Philosophy

This repository follows a simple engineering workflow:

```text
Learn
  ↓
Experiment
  ↓
Build a Small Example
  ↓
Validate the Concept
  ↓
Document the Result
  ↓
Keep It as a Reference
  ↓
Reuse in Real Projects
```

The samples are intentionally small and focused.

They are not intended to represent complete production applications. Instead, they demonstrate how a particular technology, pattern, or technique can be implemented and used.

---

## 📚 Repository Scope

The repository contains practical examples across several areas of modern .NET development.

### .NET & C#

Examples related to the core .NET platform and C# language:

* Async / Await
* `Task`
* `CancellationToken`
* Dependency Injection
* Configuration
* Options Pattern
* Streams
* HTTP
* Serialization
* Exception Handling
* Background Processing
* Concurrency
* Collections and LINQ
* Other language and framework features

---

### ASP.NET Core

Examples related to building modern ASP.NET Core applications:

* Web API
* Controllers
* Middleware
* Dependency Injection
* Exception Handling
* HTTP Pipeline
* Model Binding
* Validation
* Authentication
* Authorization
* API Documentation
* Health Checks
* Application Configuration

---

### Data Access & EF Core

Practical examples for persistence and data-access patterns:

* Entity Framework Core
* DbContext
* Repository Pattern
* Unit of Work
* Transactions
* Querying
* Relationships
* Tracking / No Tracking
* Specifications
* Interceptors
* Concurrency
* Query optimization
* Database-related experiments

---

### Messaging & Integration

Examples involving distributed communication and messaging:

* RabbitMQ
* Message Producers
* Message Consumers
* Queues
* Exchanges
* Routing
* Message handling
* Retry strategies
* Integration patterns

Additional messaging technologies may be added as the repository evolves.

---

### Logging & Observability

Examples related to application diagnostics:

* Serilog
* Structured Logging
* Request Logging
* Correlation IDs
* Trace IDs
* Application Diagnostics
* Logging Enrichers
* Centralized Logging concepts

---

### API Documentation

Examples and integrations for documenting APIs:

* Swagger / OpenAPI
* Scalar
* API metadata
* API documentation configuration

---

### Infrastructure & Supporting Libraries

Examples involving infrastructure-level components and reusable libraries:

* RabbitMQ libraries
* API documentation libraries
* HTTP utilities
* Supporting infrastructure components
* Reusable .NET extensions

---

### Design Patterns & Architecture

The repository may also contain isolated implementations of common engineering patterns:

* Repository Pattern
* Unit of Work
* Specification Pattern
* Factory Pattern
* Strategy Pattern
* Decorator Pattern
* CQRS
* Dependency Injection
* Clean Architecture concepts
* Other architectural experiments

---

# 🗂️ Repository Organization

The repository is organized around independent concepts rather than a single application domain.

A typical structure looks like:

```text
DotNet-Engineering-Lab/
│
├── README.md
│
├── Samples/
│   │
│   ├── CancellationToken/
│   │
│   ├── RabbitMQ/
│   │
│   ├── UnitOfWork/
│   │
│   ├── DependencyInjection/
│   │
│   └── ...
│
├── Libraries/
│   │
│   ├── RabbitMQ/
│   │
│   ├── Swagger/
│   │
│   ├── Scalar/
│   │
│   └── ...
│
├── Experiments/
│   │
│   └── ...
│
└── Apps/
    │
    └── ...
```

The exact structure may evolve as new technologies and concepts are added.

---

# 🧪 Sample-Based Development

Each sample should answer one specific engineering question.

For example:

```text
CancellationToken
```

focuses on cancellation and cooperative cancellation.

```text
RabbitMQ
```

focuses on messaging and asynchronous communication.

```text
UnitOfWork
```

focuses on transaction boundaries and persistence abstraction.

The intention is to avoid creating unnecessarily large applications when a small isolated example can demonstrate the concept more effectively.

---

# 📦 Sample Structure

A well-structured sample should ideally contain:

```text
SampleName/
│
├── README.md
│
├── SampleName.sln
│
├── SampleName.Project/
│   ├── Program.cs
│   ├── Services/
│   ├── Controllers/
│   └── ...
│
└── docker-compose.yml
```

Not every sample requires all of these files.

The structure should match the complexity of the concept being demonstrated.

---

# 📖 Sample Documentation

Each significant sample should contain its own `README.md`.

A sample README should preferably explain:

```text
Purpose
Concepts Demonstrated
Architecture
Prerequisites
Configuration
How to Run
How It Works
Example Usage
Expected Result
Production Considerations
Related Concepts
```

This makes each example independently understandable.

---

# ▶️ Running a Sample

Because this repository contains multiple independent projects, samples should generally be executed individually.

For a typical .NET project:

```bash
dotnet restore
dotnet build
dotnet run
```

For a Web API sample:

```bash
dotnet run
```

Then use the API endpoint or documentation interface described in that sample's README.

Some samples may require additional infrastructure such as:

* SQL Server
* PostgreSQL
* RabbitMQ
* Redis
* Docker

When external infrastructure is required, the sample should document the required setup.

---

# 🐳 Docker

Some samples may use Docker to provide their infrastructure dependencies.

For example:

```text
Application
     │
     ├── SQL Server
     │
     ├── RabbitMQ
     │
     └── Redis
```

Where appropriate, Docker Compose can be used to make infrastructure dependencies easy to start locally.

Example:

```bash
docker compose up -d
```

The exact Docker requirements are documented in the corresponding sample.

---

# 🔬 Experiments vs Production Code

An important distinction is made between experimentation and production-ready implementation.

### Experimental Code

Experimental samples exist to:

* Understand a technology
* Test an idea
* Compare approaches
* Validate behavior
* Explore an API

They may intentionally be simplified.

### Production-Oriented Code

Some samples demonstrate approaches intended to be closer to real enterprise implementations.

However:

> A sample in this repository should not automatically be considered production-ready code.

Production systems require additional considerations such as:

* Security
* Performance
* Monitoring
* Reliability
* Error handling
* Testing
* Configuration management
* Deployment
* Scalability
* Operational requirements

---

# 🧠 Engineering Principles

The repository follows several principles.

### Keep Examples Focused

Each sample should demonstrate a specific concept.

### Prefer Working Code

A runnable example is more valuable than a theoretical explanation alone.

### Keep Business Logic Out

These projects are primarily technical references and should avoid unnecessary business-domain complexity.

### Document Important Decisions

When an implementation choice is not obvious, explain the reason in the sample documentation.

### Prefer Realistic Examples

Examples should be small but should still resemble techniques used in real-world software systems.

### Avoid Premature Abstraction

A sample should not introduce unnecessary architecture simply to demonstrate architecture.

---

# 🏗️ Relationship to Other Projects

This repository is intentionally different from a production application.

For example:

```text
Enterprise Application
        │
        ├── Business Domain
        ├── Application Logic
        ├── Infrastructure
        └── UI
```

Whereas this repository is:

```text
.NET Engineering Lab
        │
        ├── Technology Samples
        ├── Architecture Samples
        ├── Infrastructure Samples
        ├── Design Pattern Samples
        └── Experiments
```

The engineering lab can therefore act as a **technical reference source** for larger applications.

---

# 🎓 Learning Strategy

A new concept can be added using the following process:

### 1. Identify the Concept

Example:

```text
CancellationToken
```

### 2. Define the Question

For example:

> How does request cancellation propagate through asynchronous operations?

### 3. Build the Smallest Useful Example

Create a focused sample that demonstrates the behavior.

### 4. Validate It

Run the sample and verify the expected behavior.

### 5. Document It

Explain:

* What it does
* Why it matters
* How it works
* How to run it
* What to consider in production

### 6. Keep It Available

The sample becomes a reusable technical reference.

---

# 🛠️ Technology Stack

The repository primarily focuses on the Microsoft .NET ecosystem.

Typical technologies include:

* C#
* .NET
* ASP.NET Core
* Entity Framework Core
* SQL Server
* RabbitMQ
* Docker
* Swagger / OpenAPI
* Scalar
* Serilog
* REST APIs
* Dependency Injection

The technology list evolves as new concepts are explored.

---

# 📌 Current Examples

Examples currently include concepts and components such as:

| Area          | Example                      |
| ------------- | ---------------------------- |
| Async         | CancellationToken            |
| Messaging     | RabbitMQ                     |
| Persistence   | Unit of Work                 |
| API           | ASP.NET Core Web API         |
| Documentation | Swagger                      |
| Documentation | Scalar                       |
| Logging       | Serilog                      |
| Libraries     | Reusable .NET libraries      |
| HTTP          | HTTP request handling        |
| Applications  | Supporting .NET applications |

The repository will continue to grow as new technologies and engineering concepts are evaluated.

---

# 🔄 Repository Evolution

This repository is intentionally evolving.

New examples may be added when:

* A new technology is introduced
* A production problem requires investigation
* A design pattern needs to be validated
* A library needs to be evaluated
* A framework feature needs practical testing
* A reusable implementation is identified

Old examples may also be refactored or reorganized as better approaches become available.

---

# 🚀 Long-Term Goal

The long-term goal is to build a comprehensive, practical reference for modern .NET engineering.

The repository should eventually provide examples covering:

```text
C#
.NET
ASP.NET Core
EF Core
Architecture
Design Patterns
Messaging
Distributed Systems
Caching
Observability
Security
Testing
Docker
Infrastructure
Performance
Concurrency
Integration
```

The goal is not to collect code for the sake of collecting code.

The goal is to build a **maintainable engineering knowledge base backed by executable examples**.

---

# 👨‍💻 Author

**Kamran Tajerbashi**

Software Engineer focused on:

* .NET / C#
* ASP.NET Core
* Angular
* Enterprise Software Architecture
* Clean Architecture
* Distributed Systems
* Infrastructure
* DevOps

---

# 📄 License

Add the appropriate license before using this repository as a public reusable project.

Recommended for an open-source reference repository:

```text
MIT License
```

if the intention is to allow broad reuse of the examples.

---

# ⭐ Contributing

This repository primarily serves as a personal engineering reference.

Suggestions, improvements, corrections, and technical discussions are welcome.

If you identify an issue with an example, feel free to open an issue or submit a pull request.

---

## 📚 Related Projects

This repository is part of a broader collection of software engineering projects covering:

* Enterprise application development
* Clean Architecture
* .NET
* Angular
* Docker
* Infrastructure
* Design Patterns
* Enterprise Management Systems

Each repository has a specific purpose and is maintained independently.

---

> **Learn it. Build it. Test it. Document it. Reuse it.**
