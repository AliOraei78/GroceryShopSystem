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