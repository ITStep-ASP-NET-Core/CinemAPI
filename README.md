# 🎬 CinemAPI

**CinemAPI** is a backend service built on ASP.NET Core (.NET 10), following a three-layer architecture with a dedicated domain layer. It provides a Web API for data operations (CRUD), stores files, and handles background operations via Azure Storage.

## 📐 Architecture

The project follows the **3-Layer Architecture** principle with an additional dedicated **Domain** layer, which brings the solution closer to the Clean Architecture approach:

- **CinemAPI.Domain** — domain layer: domain entities, repository and service contracts, domain exceptions.
- **CinemAPI.Infrastructure** — data access layer: `DbContext` (Entity Framework Core), Repository Pattern implementations, migrations, entity configurations.
- **CinemAPI.Application** — business logic layer: services, DTOs, mapping, validation.
- **CinemAPI.WebAPI** — presentation layer: controllers, middleware, DI registration, Scalar configuration.

**Dependency principle:** `WebAPI → Infrastructure → Application → Domain`

## 🛠 Tech Stack

| Category | Technology |
|---|---|
| Language | C# |
| Platform | ASP.NET Core (.NET 10) |
| Application type | Web API |
| ORM | Entity Framework Core |
| Database | Azure SQL Server |
| File/queue storage | Azure Storage (Blob + Queue) |
| API documentation | Scalar |
| Patterns | Repository Pattern, Dependency Injection |

## ✨ Features

- ✅ CRUD operations on domain entities
- ✅ Three-layer architecture with an isolated Domain layer
- ✅ Repository Pattern on top of EF Core for data access abstraction
- ✅ Dependency Injection for all services and repositories
- ✅ File storage (images, media) in **Azure Blob Storage**
- ✅ Asynchronous background task processing via **Azure Queue Storage**
- ✅ Interactive API documentation and testing via **Scalar**
- ✅ Database migrations via EF Core Migrations

## ☁️ Azure Storage Integration

| Service | Purpose |
|---|---|
| **Blob Storage** | File storage (movie posters, images, documents) |
| **Queue Storage** | Asynchronous background processing (e.g. uploaded file processing, notifications) |

## 🧩 Design Patterns

### Repository Pattern
Each Domain-layer entity has a corresponding repository with an interface and implementation in `DataAccess.Infrastructure`, isolating business logic from EF Core implementation details.

### Dependency Injection
All services, repositories, and Azure Storage clients are registered in the DI container in `Program.cs` / `Extensions`, ensuring loose coupling between components and simplifying testing.

---

Made with 💜 using ASP.NET Core
