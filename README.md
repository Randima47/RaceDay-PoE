# RaceDay Platform - Final PoE Solution (Parts 1, 2 & 3)

## Overview

RaceDay is a full-stack ASP.NET Core web platform designed to streamline race event operations, participant enrolments, event category management, and competition result tracking. The repository contains both a RESTful Web API backend service (`RaceDay.Api`), an automated xUnit testing suite (`RaceDay.Tests`), and a responsive ASP.NET Core MVC web client (`RaceDay.Web`).

---

## Architecture & Tech Stack

- **Framework:** ASP.NET Core (.NET 8.0 / 9.0)
- **Architecture:** Decoupled Architecture (RESTful Web API + MVC Web Client)
- **Database & ORM:** SQL Server / LocalDB managed via Entity Framework Core
- **Testing Suite:** xUnit with EF Core In-Memory database provider
- **Front-End:** ASP.NET Core MVC (Razor Views, Bootstrap 5, Custom CSS)
- **Session & State Management:** Server-side In-Memory Session Cache (`ISession`)
- **API Documentation:** Swagger / OpenAPI

### Repository Structure

RaceDay-PoE/
├── backend/
│   ├── RaceDay.Api/         
│   └── RaceDay.Tests/        
├── frontend/
│   └── RaceDay.Web/          
└── docs/                     
---

## Key Features & User Roles

### 1. Account & Authentication
- **Multi-Role Registration & Login:** Users register as either **Participant** or **Organiser**.
- **Role-Based Authorization:** Organisers access dedicated event creation tools, while Participants access enrolment and race timing features.

### 2. Organiser Dashboard
- Create and manage race events with dates, locations, descriptions, and custom banner image URLs.
- Define multiple race categories (e.g., 5km, 10km, 21km) with customizable entry fees.
- Capture official finish positions, category ranks, and completion times for enrolled participants.

### 3. Participant Experience
- Browse upcoming race events with dynamic category pricing and details.
- Self-enrol into specific event distance categories.
- Access a personalized "My Results" dashboard tracking official finish times and overall/category ranks.

---

## System Requirements & Prerequisites

Ensure the following tools are installed before running the solution:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) or higher
- [SQL Server](https://www.microsoft.com/sql-server) or SQL Server Express / LocalDB
- [Git](https://git-scm.com/)

---

## Local Setup & Execution Guide

### 1. Clone the Repository

git clone <your-repository-url>
cd RaceDay-PoE

### 2. Database Migration Setup
Navigate to the API directory and apply Entity Framework Core migrations to instantiate your local database:

cd backend/RaceDay.Api
dotnet ef database update

Running the Platform
The backend Web API and frontend MVC web application run as two distinct local applications.

Option A: Running from Terminal (Two Windows)
Terminal 1 — Backend Web API (RaceDay.Api):

cd backend/RaceDay.Api
dotnet run

Listens on http://localhost:5197 (Swagger accessible at http://localhost:5197/swagger).

Terminal 2 — Frontend MVC Client (RaceDay.Web)

cd frontend/RaceDay.Web
dotnet run

Listens on https://localhost:7237 (or your assigned local SSL port).
Option B: Running from Visual Studio

1. Open RaceDay.sln in Visual Studio.
2. Right-click the Solution in Solution Explorer $\rightarrow$ Properties.
3. Under Common Properties $\rightarrow$ Startup Project, choose Multiple startup projects.
3. Set the action for both RaceDay.Api and RaceDay.Web to Start.
4. Press F5 to build and run both projects simultaneously.

Running Automated Unit Tests
To execute the automated xUnit controller testing suite:

cd backend/RaceDay.Tests
dotnet test
Project Artifacts & Documentation
Database Schema Script: backend/RaceDay.Api/schema.sql

Entity Relationship Diagram (ERD): docs/ERD.png

API Endpoint Specification: docs/API_Endpoint_Plan.md

