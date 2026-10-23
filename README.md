# Employee Task Management API

A small interview-ready ASP.NET Core Web API project.

## Stack
- C#
- ASP.NET Core 8 Web API
- Entity Framework Core
- Swagger
- HTML/CSS/JavaScript

## Run
1. Install .NET 8 SDK.
2. Open this folder in Visual Studio or VS Code.
3. Run:
   dotnet restore
   dotnet run
4. Open the URL shown in the terminal.
5. Swagger is available at `/swagger`.
6. The simple UI is available at `/`.

## API endpoints
Employees:
- GET /api/employees
- GET /api/employees/{id}
- POST /api/employees
- PUT /api/employees/{id}
- DELETE /api/employees/{id}

Tasks:
- GET /api/tasks
- GET /api/tasks/{id}
- POST /api/tasks
- PUT /api/tasks/{id}/status?status=Completed
- DELETE /api/tasks/{id}

Note: For a one-hour interview demo, this version uses EF Core InMemory. For production, replace it with SQL Server using `UseSqlServer(...)` and a SQL Server connection string.
