# RaceDay API - Part 1

## Overview

RaceDay API is an ASP.NET Core RESTful Web API designed to manage race event operations, user registrations, event categories, and competition results. This project serves as the backend service responsible for data persistence, schema modeling, and endpoint architecture for the RaceDay platform.

## Architecture & Tech Stack

* **Framework:** ASP.NET Core Web API (.NET 8/9)
* **ORM:** Entity Framework Core
* **Database Engine:** SQL Server / LocalDB
* **Documentation:** OpenAPI / Swagger
* **Repository Structure:**

  * `/backend/RaceDay.Api` - Source code, models, database context, and schema artifacts.
  * `/docs` - Architectural diagrams (ERD) and endpoint planning documentation.

---

## Prerequisites

Make sure the following tools are installed in your environment before running the project:

* [.NET SDK](https://dotnet.microsoft.com/download) - Version 8.0 or higher
* [SQL Server](https://www.microsoft.com/sql-server) or SQL Server Express / LocalDB
* [Git](https://git-scm.com/)

---

## Local Setup & Installation

### 1. Clone the Repository

```bash
git clone <your-repository-url>
cd RaceDay-PoE
```

### 2. Navigate to the Backend Project

```bash
cd backend/RaceDay.Api
```

### 3. Restore Dependencies

```bash
dotnet restore
```

### 4. Build the Solution

```bash
dotnet build
```

---

## Database Configuration

The application uses **Entity Framework Core** for Object-Relational Mapping (ORM).

* **Fluent API:** Model configurations, entity relationships, and column precisions, such as decimal definitions for fees and distances, are configured in `ApplicationDbContext.cs`.
* **Manual Schema Export:** A standalone database initialization script is available at `backend/RaceDay.Api/schema.sql` for manual schema inspection and database creation.

---

## Execution & API Testing

### 1. Run the Application

```bash
dotnet run
```

### 2. Access Swagger UI

Once the application starts, navigate to the following URL in your web browser to test and inspect endpoints interactively:

```text
https://localhost:<port>/swagger
```

Replace `<port>` with the port displayed in the terminal when the application starts.

---

## Project Artifacts

* **Database Schema Script:** `backend/RaceDay.Api/schema.sql`
* **Entity Relationship Diagram (ERD):** `docs/ERD.png`
* **API Endpoint Plan:** `docs/API_Endpoint_Plan.md`
