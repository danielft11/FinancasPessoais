# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project overview

Personal-finance management REST API written in **.NET 7** following Clean Architecture. Domain language is Portuguese (`Conta`, `Lançamento`, `Cartão de Crédito`) but type names are English. Backed by **SQL Server** via **EF Core 5.0.17** and protected by ASP.NET Core **Identity + JWT** (with Google sign-in).

## Common commands

All commands run from the repository root unless stated otherwise.

```powershell
# Restore / build / run (API project is the startup project)
dotnet restore FinancasPessoais.sln
dotnet build   FinancasPessoais.sln
dotnet run     --project FinancasPessoais.Api/

# Tests (NUnit; the Tests project is currently empty — placeholder for future work)
dotnet test FinancasPessoais.sln
dotnet test --filter "FullyQualifiedName~SomeTestName"   # single test

# Stack via docker-compose (API + SQL Server on host port 1435)
docker-compose up -d
docker-compose down
```

EF Core migrations (run from repo root — `Infra.Data` is both the migrations and startup-context assembly, but the EF design tooling lives in `FinancasPessoais.Api`):

```powershell
dotnet ef migrations add <Name> --project FinancasPessoais.Infra.Data --startup-project FinancasPessoais.Api
dotnet ef database update          --project FinancasPessoais.Infra.Data --startup-project FinancasPessoais.Api
```

Note: `ApplicationDbContext` calls `Database.EnsureCreated()` in its constructor — fine for first-run dev, but it **does not apply migrations**. Run `database update` explicitly when you add migrations, otherwise schema drift is silent.

Default local dev URLs: API at `https://localhost:5001`, Swagger at `/swagger`. SQL Server expected at `localhost,1435` (the docker-compose maps host 1435 → container 1433 to avoid colliding with a local SQL Server on 1433).

## Architecture

Five class-library projects plus tests, layered strictly (outer depends inward only):

```
Api → Infra.IoC → { Application, Infra.Data } → Domain
                                    ↑
                                Domain only
```

- **`FinancasPessoais.Domain`** — entities (`BaseEntity` gives every aggregate a `Guid Id` and `CreationDate`), repository interfaces, `Utils/Constants.cs` (error-message factory + magic strings) and `Utils/Enums.cs` (e.g. `ReleaseTypes`, `Operations`). No external dependencies.
- **`FinancasPessoais.Application`** — `Services/` (one per aggregate), `Factories/` (orchestrators that compose multiple services — currently only `FinancialReleaseFactory`), `DTOs/{Requests,Responses}`, `AutoMapper/MappingProfiles.cs`, and `Interfaces/` for service contracts. Services depend on `IXxxRepository` from Domain; controllers depend on services (or factories when multi-aggregate orchestration is needed).
- **`FinancasPessoais.Infra.Data`** — `Context/ApplicationDbContext.cs` (extends `IdentityDbContext<ApplicationUser>`), `EntitiesConfiguration/` (fluent API per entity, registered in `OnModelCreating`), `Repositories/` (concrete repos extending `BaseRepository<TEntity>` for generic CRUD), `Identity/` (`ApplicationUser`, `AuthenticationService`), `Migrations/`.
- **`FinancasPessoais.Infra.IoC`** — `DependencyInjection.AddInfraStructure(services, configuration)` is the single composition root. It wires DbContext, Identity, JWT bearer, AutoMapper, Hangfire, and registers every repository + service. **When adding a new service or repository, you must register it here** — there is no assembly scanning. Also hosts `FileService` for `AccountPayable` attachments.
- **`FinancasPessoais.Api`** — Thin controllers under `Controllers/`, `Startup.cs` (classic Startup pattern — not minimal hosting), `Token/TokenService.cs` (static helper, bootstrapped from `IConfiguration` in `AuthenticationController`'s ctor). `Converters/` holds custom JSON converters.

### Authentication & per-user scoping

JWT issued by `TokenService` carries `ClaimTypes.PrimarySid` = `ApplicationUser.Id`. Controllers extract it via `User.FindFirst(ClaimTypes.PrimarySid)?.Value` (see `LoggedUserId()` in `FinancialReleaseController`) and **pass it explicitly into service/factory methods** — services never read `HttpContext`. Any new endpoint that creates or queries user-owned data must propagate this id and filter by it; `FinancialRelease.UserId` is the existing precedent.

Google sign-in lives in `AuthenticationController.GoogleLogin` and validates the ID token via `GoogleJsonWebSignature.ValidateAsync`. New external-login providers should follow the same "find-or-create `ApplicationUser`, then issue our JWT" flow.

### Factories vs services

`Factories/` orchestrate cross-aggregate workflows that would otherwise leak between services (e.g. `FinancialReleaseFactory` validates accounts/credit-cards/balance before delegating to `FinancialReleaseService`). When a controller action needs to coordinate multiple services with business preconditions, add a factory rather than fattening one service or doing it in the controller.

### Hangfire

Hangfire is registered in `DependencyInjection.AddHangfire()` using SQL Server storage on the same connection string, but the dashboard and the recurring job for `AccountPayableService.CheckAndSendReminders` are currently **commented out** in `Startup.Configure`. Re-enable both together when activating reminders.

## Configuration

`appsettings.json` keys that matter:

- `ConnectionStrings:DefaultConnection` — SQL Server. Docker compose overrides this to point at the `sqlserver` service.
- `Jwt:{SecretKey,Issuer,Audience}` — symmetric HS256 signing; `TokenService` reads these directly.
- `FileStorage:BasePath` — local disk path used by `FileService` for accounts-payable attachments (default `D:/Documents/AccountsPayable` — Windows path, change for Linux/container).
- `Frontend:ResetPasswordUrl` — used to build the password-reset callback link sent by `EmailService`. Defaults to an Angular dev server on `localhost:4200`.

CORS in `Startup.cs` whitelists `https://localhost:7167` and `http://localhost:4200` — update there when adding a new frontend origin.
