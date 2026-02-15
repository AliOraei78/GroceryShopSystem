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

## Day 7: MediatR & FluentValidation Integration  
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

## Day 8: Testing in Clean Architecture  
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

## Day 9: Refactoring & Anti-Patterns  
Avoiding Anemic Domain, God Classes, Fat Controllers & More

**Completed Today:**
- Identified and refactored Anemic Domain Model in Product entity
  - Enriched domain with behavior (ReduceStock, IncreaseStock, UpdatePrice)
  - Moved validation to constructor and domain methods
  - Introduced Guard class for readable invariants
- Eliminated God Class risk in handlers
  - Separated concerns: validation (FluentValidation), domain creation, persistence
- Prevented Fat Controller pattern
  - Controller only mediates (sends commands/queries via MediatR)
  - No business logic or validation in controller
- Applied refactoring best practices:
  - Tell, Don't Ask → domain methods instead of get/set + external logic
  - Encapsulated business rules in entity (not in service/handler)
  - Improved expressiveness of domain language
- Verified refactoring with unit tests (Core & Application layers)

**Key Learnings:**
- Anemic Domain Model: data-only classes → move behavior to entities
- God Class: one class doing everything → split responsibilities
- Fat Controller: logic in controller → delegate to MediatR handlers
- Tell, Don't Ask: entities should protect invariants internally
- Refactoring goal: domain should speak its own language (ubiquitous language)

## Day 10: Performance & Scalability  
Caching, Async, Background Jobs – Optimization with Redis and Hangfire

**Completed Today:**
- Implemented full performance and scalability improvements across GroceryShopSystem
- Added **Redis caching** for frequently accessed queries (e.g., GetAllProducts)
  - Used `IDistributedCache` with StackExchange.Redis
  - Cached query results with absolute + sliding expiration (5 min / 2 min)
  - Reduced database load for read-heavy operations
- Ensured **full async/await** usage throughout the stack
  - Applied `ConfigureAwait(false)` in non-UI contexts to prevent deadlocks
  - All repository calls, handler logic, and external calls made fully asynchronous
- Integrated **Hangfire** for background job processing
  - Added Hangfire with Redis storage
  - Created sample background job: `SendProductAddedEmailJob`
  - Enqueued job after successful product creation (fire-and-forget)
  - Enabled Hangfire dashboard at `/hangfire` for job monitoring
- Tested real-world impact:
  - Measured response time improvement with caching enabled
  - Verified background job execution in Hangfire dashboard
  - Confirmed async pipeline handles high concurrency without blocking
- Updated configuration:
  - Added Redis connection string in appsettings.json
  - Registered services in Program.cs (Redis cache + Hangfire server/dashboard)

**Key Learnings:**
- **Caching**: Dramatically reduces latency and DB pressure for read operations
  - Cache key design, expiration policies, and invalidation are critical
- **Async/await**: Prevents thread pool starvation and improves throughput
  - Always use `ConfigureAwait(false)` in library code
- **Hangfire**: Ideal for fire-and-forget or scheduled tasks (emails, reports, order processing)
  - Redis storage provides durability and scalability
  - Dashboard offers excellent visibility into job queue and history
- Performance gains are measurable: faster API responses, non-blocking I/O, offloaded heavy work

## Day 11: Complete GroceryShopSystemAPI Implementation with Vertical Slice

**Completed Today:**
- Finalized full implementation of **GroceryShopSystemAPI** using **Clean Architecture + Onion + Vertical Slice**
- Completed the **Products** vertical slice with all required components:
  - Commands: `AddProductCommand` + `AddProductCommandHandler` + `AddProductCommandValidator`
  - Queries: `GetAllProductsQuery` + `GetAllProductsQueryHandler`
  - DTOs: `ProductDto` for clean output projection
  - Validators: FluentValidation rules for input validation
- Implemented Infrastructure layer with real persistence:
  - `AppDbContext` (EF Core DbContext)
  - `ProductConfiguration` (EntityTypeConfiguration for owned types and constraints)
  - `ProductRepository` (real EF Core implementation of `IProductRepository`)
- Registered all dependencies in `Program.cs`:
  - DbContext with Npgsql provider
  - MediatR handlers from Application assembly
  - FluentValidation validators
  - Repository implementations
- Created and applied initial migration:
  - `dotnet ef migrations add InitialCreate`
  - `dotnet ef database update`
- Verified end-to-end functionality:
  - POST /api/products → creates new product in real database
  - GET /api/products → returns list of ProductDto from database
  - Data persists across restarts (PostgreSQL volume)
  - Validation errors return 400 Bad Request with detailed messages
- Updated README with complete architecture overview, structure, and setup instructions

**Key Learnings:**
- Vertical Slice + Clean/Onion delivers cohesive, feature-focused code while maintaining separation of concerns
- Core remains pure: only entities, value objects, and domain rules
- Application owns use-cases: commands, queries, handlers, validators, DTOs
- Infrastructure owns technical details: DbContext, EF configurations, concrete repositories
- MediatR decouples presentation from application logic
- FluentValidation ensures input validation at application boundary
- EF Core migrations enable real persistence from day one

## Day 12: Security & Authentication (JWT, Roles in Clean) – Integrating Auth Across Layers

**Completed Today:**
- Implemented full **JWT-based authentication** and **role-based authorization** within Clean Architecture
- Defined domain-level security model in Core:
  - `User` entity with Id, Email, PasswordHash, Role
  - `IPasswordHasher` abstraction for secure hashing
- Provided concrete implementations in Infrastructure:
  - `BcryptPasswordHasher` using BCrypt.Net-Next
  - `JwtTokenService` for generating secure JWT tokens with claims (Id, Email, Role)
- Created authentication feature slice in Application:
  - `LoginCommand` + `LoginCommandHandler` for credential validation and token generation
  - Integrated password verification and token issuance
- Configured JWT authentication middleware in Api project:
  - Added JwtBearer scheme with proper token validation parameters
  - Registered `AddAuthentication` and `AddAuthorization`
- Protected endpoints with `[Authorize]` and role checks:
  - Example: `[Authorize(Roles = "Admin")]` on admin-only endpoints
- Seeded an initial admin user via `AppDbContextSeed` (Development environment)
  - Email: admin@grocery.com
  - Password: Admin123! (hashed with BCrypt)
- Tested authentication flow:
  - Successful login → returns valid JWT token
  - Protected endpoints: 401 Unauthorized without token, 403 Forbidden with wrong role
  - Token validation and role enforcement working correctly

**Key Learnings:**
- Authentication and authorization cleanly integrated into Clean/Onion layers:
  - Core owns domain security model (User entity)
  - Application owns use-case (LoginCommand + Handler)
  - Infrastructure provides concrete security services (hasher, token generator)
  - Presentation (Api) handles HTTP concerns and middleware
- JWT tokens carry claims (Id, Email, Role) for role-based access control
- Passwords never stored in plain text — always hashed with strong algorithm (BCrypt)
- Seeding sensitive data (admin user) only in Development environment
- Role-based protection prevents unauthorized access to sensitive operations