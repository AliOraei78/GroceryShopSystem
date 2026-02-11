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
ترکیب Vertical Slice با Clean/Onion

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