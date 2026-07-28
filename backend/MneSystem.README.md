# M&E System - Monitoring & Evaluation System

A production-ready ASP.NET Core 9 Web API solution following Clean Architecture principles.

## Solution Structure

```
MneSystem.sln
├── src/
│   ├── MneSystem.Api/          # Presentation layer - Web API
│   ├── MneSystem.Application/  # Application layer - Business logic, MediatR, AutoMapper, FluentValidation
│   ├── MneSystem.Domain/       # Domain layer - Entities, value objects, interfaces
│   └── MneSystem.Infrastructure/# Infrastructure layer - Data access, external services
└── tests/
    └── MneSystem.UnitTests/    # Unit tests
```

## Technology Stack

- **.NET 9.0** - Latest framework
- **ASP.NET Core 9** - Web API framework
- **PostgreSQL** - Database
- **Entity Framework Core 9** - ORM
- **ASP.NET Identity** - Authentication and authorization (not yet implemented)
- **MediatR** - CQRS and mediator pattern
- **FluentValidation** - Request validation
- **AutoMapper** - Object mapping
- **Serilog** - Structured logging
- **Swagger/OpenAPI** - API documentation
- **xUnit** - Testing framework
- **Moq** - Mocking framework
- **FluentAssertions** - Assertion library

## Architecture Layers

### Domain Layer
- Contains business entities and value objects
- No external dependencies
- Core business rules and interfaces

### Application Layer
- Contains application services and business logic
- MediatR for CQRS pattern
- AutoMapper for object mapping
- FluentValidation for request validation
- Depends on Domain layer

### Infrastructure Layer
- Contains data access implementation
- External service integrations
- Database context and migrations
- Depends on Application and Domain layers

### API Layer
- Presentation layer
- Controllers and middleware
- Swagger documentation
- Depends on Application and Infrastructure layers

## Configuration

### Database Connection
Update `appsettings.json` with your PostgreSQL connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=mne_system;Username=postgres;Password=your_password"
  }
}
```

### Serilog Logging
Configured in `appsettings.json` with:
- Console sink
- File sink (rolling daily)
- Multiple log levels for different components

### CORS
Configured to allow all origins in development. Update `CorsConfiguration.cs` for production.

## Running the Application

1. Ensure PostgreSQL is running
2. Update connection string in `appsettings.json`
3. Restore NuGet packages
4. Run migrations (when implemented)
5. Start the API

```bash
dotnet restore
dotnet run --project src/MneSystem.Api
```

## API Documentation

Swagger UI is available at:
- Development: `https://localhost:5001/` (default)

## Testing

Run unit tests:

```bash
dotnet test
```

## Features Implemented

- ✅ Clean Architecture structure
- ✅ ASP.NET Core 9 Web API
- ✅ PostgreSQL with Entity Framework Core
- ✅ Serilog structured logging
- ✅ Swagger/OpenAPI documentation
- ✅ Global exception handling middleware
- ✅ CORS configuration
- ✅ Dependency injection setup
- ✅ Repository pattern base implementation
- ✅ MediatR integration
- ✅ AutoMapper integration
- ✅ FluentValidation integration
- ✅ Nullable reference types enabled
- ✅ Global using statements
- ✅ Health check endpoint
- ✅ Sample weather forecast endpoint

## Next Steps

- Implement authentication with ASP.NET Identity
- Create database migrations
- Implement business entities in Domain layer
- Create CQRS handlers in Application layer
- Implement repository implementations
- Add API endpoints for business operations
- Write comprehensive unit tests
- Add integration tests
- Implement rate limiting
- Add API versioning
- Implement caching strategy

## License

This project is part of the Mini-Dynamic-Survey-Application backend.