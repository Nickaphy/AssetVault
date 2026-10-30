# AssetVault — Product Backlog

## Project Overview
**Project:** AssetVault — IT Asset Management System  
**Role:** Junior .NET Developer  
**Stack:** C#, .NET 10, ASP.NET Core, Blazor, EF Core  
**Architecture:** Hexagonal Architecture (Ports & Adapters)

### Objective
Build a complete full-stack IT asset management application from scratch, 
progressively introducing professional software architecture, REST APIs, databases, 
frontend development, testing, and deployment.

## Sprint Overview

| Sprint | Focus                  | Deliverable                    |
| ------ | ---------------------- | ------------------------------ |
| 1      | C# Fundamentals        | Functional console application |
| 2      | Hexagonal Architecture | Layered application with DI    |
| 3      | Persistence                   | Working database               |
| 4      | REST API               | HTTP endpoints and OpenAPI     |
| 5      | Blazor                 | Full-stack web application     |
| 6      | Production Engineering | Tests, security, Docker, CI/CD |

---

## Sprint 1 — Core C# and Console Application

**Goal:** Build a functional asset management system using fundamental C# concepts.

### AV-001 — Register Assets
As an IT administrator, I want to register equipment so that the organization 
can maintain an inventory of its assets.

Each asset must contain:

| Property     | Type        | Constraint              |
| ------------ | ----------- | ----------------------- |
| ID           | `Guid`      | Automatically generated |
| Name         | `string`    | Required, non-empty     |
| Type         | `AssetType` | Laptop, Desktop, Server |
| PurchaseDate | `DateTime`  | Cannot be in the future |
| IsAssigned   | `bool`      | Defaults to false       |

**Acceptance Criteria**
- [x] Application launches from the terminal.
- [x] User can register an asset through console input.
- [x] Invalid names are rejected.
- [x] Future purchase dates are rejected.
- [x] Invalid asset types are rejected.
- [x] Asset receives a unique ID automatically.
- [x] Registered assets are stored in memory.

### AV-002 — View Assets
- [x] List all registered assets.
- [x] Display asset properties.
- [x] Display an appropriate message when no assets exist.

### AV-003 — Find Assets
- [x] Search for an asset by its ID.
- [x] Display the matching asset.
- [x] Handle nonexistent assets gracefully.

### AV-004 — Remove Assets
- [x] Remove an asset by its ID.
- [x] Reject removal requests for nonexistent assets.
- [ ] Verify the asset has been removed.

### AV-005 — Console Navigation
- [ ] Implement an interactive console menu.
- [ ] Validate user input.
- [ ] Allow multiple operations without restarting.
- [ ] Provide an exit option.

### Technical Constraints
- C# and .NET 10.
- Console application.
- In-memory collections.
- No database.
- No external frameworks.
- No unnecessary abstractions.

**Definition of Done:** All five features work correctly, invalid inputs are handled, and the developer can explain the implementation.

---

## Sprint 2 — Hexagonal Architecture

**Goal:** Refactor the application into a maintainable architecture without changing its functionality.

### Requirements
- [ ] Separate Domain, Application, Infrastructure, and Console projects.
- [ ] Introduce application services/use cases.
- [ ] Define repository ports using interfaces.
- [ ] Implement an in-memory repository adapter.
- [ ] Configure dependency injection.
- [ ] Separate business logic from console input/output.
- [ ] Ensure dependencies point toward the application core.

### Concepts
- Separation of concerns
- Dependency inversion
- Dependency injection
- Ports and adapters
- Domain models
- Application services
- Repository pattern

**Definition of Done:** The console interface or storage implementation can be replaced without rewriting business logic.

---

## Sprint 3 — Persistence

**Goal:** Replace temporary in-memory storage with persistent database storage.

### Requirements
- [ ] Introduce Entity Framework Core.
- [ ] Configure SQLite.
- [ ] Implement a database repository adapter.
- [ ] Create and apply database migrations.
- [ ] Persist registered assets.
- [ ] Retrieve stored assets.
- [ ] Handle database failures appropriately.
- [ ] Verify assets survive application restarts.

### Concepts
- Object-relational mapping
- Database contexts
- SQL fundamentals
- Database migrations
- Persistence abstractions
- Repository implementations

**Definition of Done:** Assets persist between application restarts, and persistence logic remains isolated behind repository ports.

---

## Sprint 4 — REST API

**Goal:** Expose AssetVault functionality through HTTP using ASP.NET Core.

### Requirements
- [ ] Create an ASP.NET Core Web API project.
- [ ] Implement REST controllers.
- [ ] Introduce request and response DTOs.
- [ ] Implement appropriate HTTP status codes.
- [ ] Configure dependency injection.
- [ ] Connect controllers to application services.
- [ ] Add OpenAPI documentation.
- [ ] Validate requests.
- [ ] Handle errors consistently.

### Required Endpoints

| Method | Endpoint | Purpose |
|---|---|---|
| GET | `/api/assets` | List all assets |
| GET | `/api/assets/{id}` | Retrieve an asset |
| POST | `/api/assets` | Register an asset |
| DELETE | `/api/assets/{id}` | Remove an asset |

### Concepts
- REST principles
- HTTP methods and status codes
- Controllers
- Routing
- DTOs
- Serialization
- Middleware
- OpenAPI

**Definition of Done:** Every asset operation can be performed through HTTP without modifying domain or application logic.

---

## Sprint 5 — Blazor Frontend

**Goal:** Build a graphical interface for managing IT assets.

### Requirements
- [ ] Create a Blazor web application.
- [ ] Display registered assets in a table.
- [ ] Implement asset registration forms.
- [ ] Implement asset details.
- [ ] Implement asset deletion.
- [ ] Communicate with the REST API using `HttpClient`.
- [ ] Handle loading states.
- [ ] Display validation errors.
- [ ] Handle API failures.
- [ ] Separate UI logic from application logic.

### Concepts
- Razor components
- Component lifecycle
- Data binding
- Form validation
- HTTP clients
- Frontend/backend communication
- Component composition

**Definition of Done:** Users can manage assets through the browser without interacting directly with the console or API testing tools.

---

## Sprint 6 — Production Engineering

**Goal:** Introduce professional development, security, testing, and deployment practices.

### Requirements
- [ ] Implement unit tests.
- [ ] Implement integration tests.
- [ ] Introduce authentication.
- [ ] Implement authorization.
- [ ] Protect asset management endpoints.
- [ ] Containerize the application using Docker.
- [ ] Configure Docker Compose.
- [ ] Implement a GitHub Actions CI pipeline.
- [ ] Introduce environment-based configuration.
- [ ] Implement structured logging.
- [ ] Document build, test, and deployment procedures.

### Concepts
- Unit testing
- Integration testing
- Authentication and authorization
- Docker and containers
- CI/CD
- Configuration management
- Logging and observability
- Application security

**Definition of Done:** The application is tested, access-controlled, containerized, and automatically validated through CI.

---

## Development Rules

1. The developer owns the implementation.
2. The Product Owner provides requirements and reviews implementations.
3. Work on one ticket at a time.
4. Avoid abstractions without a justified responsibility.
5. Every ticket must satisfy its acceptance criteria.
6. Progress depends on demonstrated understanding, not deadlines.
7. The developer must be able to explain architectural decisions.
8. AI may be used for explanations, debugging, and reviews, but not to generate entire implementations.

## Current Assignment

**Sprint:** 1 — Core C#  
**Ticket:** AV-001 — Register Assets  
**Status:** In Progress

**Next Deliverable:** Implement asset registration with validated console input, enum conversion, date validation, automatically generated IDs, and in-memory storage.