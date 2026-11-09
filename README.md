# RaceDay API - Part 1 & Part 2

## Overview

RaceDay API is an ASP.NET Core RESTful Web API designed to manage race event operations, user registrations, event categories, and competition results. This project serves as the backend service responsible for data persistence, schema modeling, and endpoint architecture for the RaceDay platform. 

**Part 2 Continuation:** Expands upon the foundational architecture by introducing automated xUnit testing suites (`RaceDay.Tests`), expanded controller logic, and robust Entity Framework Core test integration.

## Architecture & Tech Stack

- **Framework:** ASP.NET Core Web API (.NET 8/9)
- **ORM:** Entity Framework Core
- **Database Engine:** SQL Server / LocalDB / EF Core In-Memory (for testing)
- **Testing Framework:** xUnit (Part 2)
- **Documentation:** OpenAPI / Swagger
- **Repository Structure:**

  * `/backend/RaceDay.Api` - Source code, models, database context, and schema artifacts.
  * `/backend/RaceDay.Tests` - Automated xUnit test project for controller logic verification (Part 2).
  * `/docs` - Architectural diagrams (ERD) and endpoint planning documentation.

---

## Prerequisites

Ensure the following tools are installed in your environment before running the project:

- [.NET SDK](https://dotnet.microsoft.com/download) - Version 8.0 or higher
- [SQL Server](https://www.microsoft.com/sql-server) or SQL Server Express / LocalDB
- [Git](https://git-scm.com/)

---

## Local Setup & Installation

### 1. Clone the Repository

```bash
git clone <your-repository-url>
cd RaceDay-PoE
2. Navigate to the Backend Project
Bash
cd backend/RaceDay.Api
3. Restore Dependencies
Bash
dotnet restore
4. Build the Solution
Bash
dotnet build
Database Configuration
The application uses Entity Framework Core for Object-Relational Mapping (ORM).

Fluent API: Model configurations, entity relationships, and column precisions, such as decimal definitions for fees and distances, are configured in ApplicationDbContext.cs.

Manual Schema Export: A standalone database initialization script is available at backend/RaceDay.Api/schema.sql for manual schema inspection and database creation.

Execution & API Testing
1. Run the Application
Bash
dotnet run
2. Access Swagger UI
Once the application starts, navigate to the following URL in your web browser to test and inspect endpoints interactively:

Plaintext
https://localhost:<port>/swagger
Replace <port> with the port displayed in the terminal when the application starts.

Part 2: Automated Testing Execution
To run the automated unit testing suite introduced in Part 2:

Navigate to the root backend folder or RaceDay.Tests directory.

Execute the test runner command:

Bash
dotnet test
Project Artifacts
Database Schema Script: backend/RaceDay.Api/schema.sql

Automated Test Project: backend/RaceDay.Tests/ (Part 2)

Entity Relationship Diagram (ERD): docs/ERD.png

API Endpoint Plan: docs/API_Endpoint_Plan.md
