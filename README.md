# Task-Management-API

A RESTful Task Management API built with ASP.NET Core and Entity Framework Core.

## Features

- JWT authentication (`POST /api/auth/token`)
- Role-based authorization (`Admin` required for task deletion)
- CRUD operations for tasks (`/api/tasks`)
- Request validation via data annotations
- Structured logging via `ILogger`
- Swagger/OpenAPI documentation with ****** support
- SQL Server integration through Entity Framework Core
- Clean Architecture-inspired separation (`Application`, `Domain`, `Infrastructure`, `Controllers`)

## Run

```bash
dotnet restore TaskManagement.slnx
dotnet run --project /home/runner/work/Task-Management-API/Task-Management-API/src/TaskManagement.Api/TaskManagement.Api.csproj
```

## Test

```bash
dotnet test TaskManagement.slnx
```
