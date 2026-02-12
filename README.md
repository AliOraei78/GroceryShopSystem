## Day 1: Clean Architecture Basics – Principles, Layers, Dependency Inversion

**Completed Today:**
- Studied core principles of Clean Architecture (independence from frameworks/UI/DB/external)
- Explored main layers: Entities, Use Cases, Adapters, Frameworks & Drivers
- Understood Dependency Inversion Principle (DIP) and its role in testable code
- Designed simple layered structure (theoretical, no code yet)
- Prepared mindset for Onion & Vertical Slice integration

**Key Learnings:**
- Clean Arch keeps core business logic independent and testable
- Layers follow Dependency Rule: outer depends on inner
- DIP inverts dependencies using interfaces (high-level owns the interface)

## Day 2: Onion Architecture – Core, Application, Infrastructure, Presentation

**Completed Today:**
- Established full project structure for **GroceryShopSystem** using **Onion Architecture** integrated with **Clean Architecture** principles
- Created four main projects with clear dependency flow:
  - **Core**: Pure domain entities, value objects, domain interfaces, and business rules (no external dependencies)
  - **Application**: Use cases, DTOs, application services, interfaces for repositories and external services
  - **Infrastructure**: Concrete implementations (EF Core DbContext, repositories, external integrations)
  - **Api (Presentation)**: ASP.NET Core Web API, controllers, minimal DI setup
- Implemented **Dependency Inversion**:
  - High-level modules (Application) depend on abstractions (interfaces in Core/Application)
  - Low-level modules (Infrastructure) implement those abstractions
  - Direction of dependency always points inward (toward Core)
- Added basic entity (`Product`) in Core with domain validation logic
- Defined repository interface (`IProductRepository`) in Application
- Provided temporary in-memory implementation in Infrastructure
- Registered DI in Api project (Program.cs)
- Tested basic endpoint `/api/products` (GET all products)

**Key Learnings:**
- Onion Architecture places domain logic at the center and layers outward
  - Core → Application → Infrastructure → Presentation
- Clean Architecture principles applied: independence from frameworks, testability, UI/DB independence
- Dependency Rule strictly enforced: inner layers know nothing about outer layers
- Application layer owns use-case interfaces → Infrastructure provides implementations
- Core remains pure and framework-agnostic (no EF Core, no ASP.NET references)

## Day 3: Vertical Slice Architecture – Feature-based slicing vs horizontal layers  
Integrating Vertical Slice with Clean/Onion Architecture

**Completed Today:**
- Deep comparison between horizontal layering and vertical slice architecture
- Identified key limitations of traditional layered approach in large-scale projects (tight coupling, scattered feature code, hard refactoring)
- Introduced Vertical Slice as a feature-centric organization method
- Refactored project structure toward vertical slices:
  - Moved from horizontal folders (Commands/Queries/Services global) to feature-based folders
  - Created first vertical slice: **Products** (Commands, Queries, DTOs, Validators all in one folder)
- Combined Vertical Slice with Clean/Onion principles:
  - Core layer remains pure and central (Entities, domain logic)
  - Application layer owns feature slices (use cases per feature)
  - Infrastructure provides concrete implementations (repositories, persistence)
  - Presentation (API) consumes slices via endpoints
- Added basic command/query placeholders for Product feature
- Updated dependency flow to support slice-based design
- Tested basic endpoint `/api/products` (GET all) after refactoring

**Key Learnings:**
- Horizontal layers organize code by technical role → leads to scattered feature logic
- Vertical Slice organizes code by business feature → all code for one use case stays together
- Benefits of Vertical Slice:
  - Easier feature addition/removal/refactoring
  - Better team parallelism (teams own slices)
  - Reduced cognitive load (no jumping between layers)
  - Improved testability (slice contains everything needed)
- Integration with Clean/Onion:
  - Core still independent (no framework references)
  - Application owns slice interfaces and handlers
  - Dependency Rule preserved: outer layers depend on inner abstractions
- First vertical slice created: Product Management (Commands/Queries/DTOs)

## Day 4: SOLID Principles in Clean Architecture

**Completed Today:**
- Deep dive into SOLID principles with Clean Architecture focus
- Applied each principle to GroceryShopSystem:
  - S: Single Responsibility → separated validation logic from entity
  - O: Open-Closed → introduced IValidator<T> for extensible validation
  - L: Liskov Substitution → ensured repository implementations are interchangeable
  - I: Interface Segregation → kept IProductRepository small and focused
  - D: Dependency Inversion → high-level depends on abstractions only
- Refactored code to better align with SOLID:
  - Added ProductValidator in Application
  - Introduced IValidator<T> interface
  - Verified no direct dependencies in Core/Application to Infrastructure
- Tested endpoint `/api/products` after refactoring

**Key Learnings:**
- SOLID ensures maintainable, testable, and flexible code in Clean Arch
- Single Responsibility: one class = one reason to change
- Open-Closed: extend behavior without modifying existing code
- Liskov: subtypes must be substitutable without breaking behavior
- Interface Segregation: many small interfaces > one large interface
- Dependency Inversion: depend on abstractions, not concretions

## Day 5: DDD Basics – Entities, Value Objects, Aggregates, Repositories  
Integration of DDD into the Core Layer

**Completed Today:**
- Studied core DDD concepts:
  - **Entity**: objects with identity (e.g., Product with Guid Id)
  - **Value Object**: immutable objects defined by values (e.g., Money with Amount + Currency)
  - **Aggregate**: consistency boundary – group of Entities + Value Objects accessed via Aggregate Root
  - **Repository**: abstraction for retrieving/storing Aggregates (only Root, not internal Entities)
- Refactored Product in Core:
  - Converted Price to Money Value Object
  - Added domain behavior (ReduceStock, IncreaseStock)
  - Enforced invariants (validation in constructor)
- Defined IProductRepository in Application layer (abstraction)
- Provided in-memory implementation in Infrastructure
- Ensured Core remains pure: no EF Core, no external references

**Key Learnings:**
- Entities have identity and lifecycle
- Value Objects are immutable and compared by value
- Aggregates protect invariants – only accessed via Root
- Repositories return Aggregates, not individual Entities
- Core layer is now DDD-rich: business rules encapsulated

## Day 6: CQRS in Vertical Slice – Commands, Queries, Handlers  
Implementing CQRS within Vertical Slices

**Completed Today:**
- Introduced CQRS (Command Query Responsibility Segregation) as core pattern in Vertical Slice
- Differentiated Command (write, side-effect) vs Query (read-only)
- Created first Command: `AddProductCommand` (in Products/Commands)
- Created first Query: `GetAllProductsQuery` (in Products/Queries)
- Added DTO: `ProductDto` for query results
- Updated `IProductRepository` to support query methods
- Refactored ProductsController to use repository (placeholder – MediatR next)
- Prepared structure for handlers (Commands/Queries + Handlers per feature)

**Key Learnings:**
- CQRS separates read and write models → allows independent scaling/optimization
- Commands: change state, usually return void or Result
- Queries: read data, return DTOs or projections
- Vertical Slice + CQRS = cohesive feature folders with all related code (command + query + handler + DTO)
- Dependency flow preserved: Application owns CQRS contracts, Core remains pure

## Day 7 - Phase 7: MediatR & FluentValidation Integration  
Implementing Command and Query Handlers using MediatR with validation via FluentValidation

**Completed Today:**
- Installed and configured **MediatR** as the central mediator for processing Commands and Queries
- Added **FluentValidation** for input validation within the MediatR pipeline
- Implemented full CQRS handlers in Vertical Slice:
  - `AddProductCommand` + `AddProductCommandHandler` (creates Product entity and persists via repository)
  - `GetAllProductsQuery` + `GetAllProductsQueryHandler` (retrieves and maps to DTO)
- Created validator: `AddProductCommandValidator` with rules for Name, Price, Category, and Stock
- Refactored `ProductsController` to use MediatR instead of direct repository calls:
  - `GET /api/products` → sends `GetAllProductsQuery`
  - `POST /api/products` → sends `AddProductCommand`
- Registered MediatR and FluentValidation in DI (Program.cs)
- Tested endpoints in Swagger/Postman:
  - Successful POST with valid data → returns new Guid
  - Invalid POST → returns validation errors (400 Bad Request)
  - GET → returns list of ProductDto

**Key Learnings:**
- MediatR decouples controllers from business logic (send message → handler processes)
- Commands return result (Guid for AddProduct), Queries return data (IEnumerable<ProductDto>)
- FluentValidation integrates seamlessly with MediatR via pipeline behaviors
- Validation rules centralized in Application layer (not in controller or entity)
- Vertical Slice remains cohesive: command + handler + validator + DTO all in one feature folder
- Clean Architecture preserved: Application owns handlers and validators, Infrastructure provides repository implementations

## Day 8 - Phase 7: Testing in Clean Architecture  
Unit, Integration, and End-to-End (E2E) Testing of Layers using xUnit and Moq

**Completed Today:**
- Established a dedicated test project: **GroceryShopSystem.Tests** (xUnit)
- Installed essential testing packages:
  - xunit, xunit.runner.visualstudio, Moq, Microsoft.NET.Test.Sdk
  - Prepared for future integration: Testcontainers, WebApplicationFactory
- Wrote **Unit Tests** for Core layer:
  - Product entity creation, validation invariants, domain behavior (ReduceStock/IncreaseStock)
  - Verified exceptions for invalid data (negative price, insufficient stock)
- Wrote **Unit Tests** for Application layer:
  - AddProductCommandHandler with Moq repository
  - Tested successful addition, correct entity creation, repository interaction verification
- Introduced testing strategy per layer:
  - Core & Application: pure unit tests (no DB, full mocking)
  - Infrastructure: integration tests (real DB or InMemory)
  - Api: end-to-end tests (WebApplicationFactory + real HTTP calls)
- Ran all tests successfully (`dotnet test`) → green results
- Updated README with testing structure, tools, and coverage goals

**Key Learnings:**
- Unit tests in Clean Arch focus on Core & Application (mock external dependencies)
- Moq used for repository isolation → verifies interactions without real DB
- Domain invariants enforced and tested at entity level (no external validation needed)
- Application handlers tested independently → ensures use-case logic is correct
- Testing pyramid applied: many unit tests, fewer integration, minimal E2E
- Tests remain fast and reliable (no real DB in unit tests)