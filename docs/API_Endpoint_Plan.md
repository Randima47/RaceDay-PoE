# RaceDay Web API Endpoint Specification Plan

| Method | Endpoint Route | Request Body / Parameters | Response Data | Auth / Authorization |
| :--- | :--- | :--- | :--- | :--- |
| **POST** | `/api/auth/register` | `{ "fullName": "...", "email": "...", "password": "...", "role": "Participant" }` | `{ "message": "User registered successfully", "userId": 1 }` | Public |
| **POST** | `/api/auth/login` | `{ "email": "...", "password": "..." }` | `{ "token": "jwt_token_string...", "role": "Organiser" }` | Public |
| **GET** | `/api/events` | None | `[ { "eventId": 1, "title": "Polokwane City Marathon", "date": "...", "location": "..." } ]` | Public |
| **GET** | `/api/events/{id}` | Route parameter: `id` | `{ "eventId": 1, "title": "...", "categories": [...] }` | Public |
| **POST** | `/api/events` | `{ "title": "...", "description": "...", "eventDate": "...", "location": "..." }` | `{ "eventId": 2, "message": "Event created" }` | Organiser Only |
| **PUT** | `/api/events/{id}` | Route parameter: `id`, `{ "title": "...", "location": "..." }` | `{ "message": "Event updated successfully" }` | Organiser Only |
| **DELETE**| `/api/events/{id}` | Route parameter: `id` | `{ "message": "Event deleted successfully" }` | Organiser Only |
| **POST** | `/api/events/{id}/categories` | Route parameter: `id`, `{ "categoryName": "...", "distanceKM": 21.1, "entryFee": 250 }` | `{ "categoryId": 1, "message": "Category added" }` | Organiser Only |
| **POST** | `/api/enrolments` | `{ "categoryId": 1 }` | `{ "enrolmentId": 10, "paymentStatus": "Paid" }` | Participant Only |
| **GET** | `/api/enrolments/my-enrolments` | None | `[ { "enrolmentId": 10, "eventTitle": "...", "categoryName": "..." } ]` | Participant Only |
| **GET** | `/api/results/event/{eventId}` | Route parameter: `eventId` | `[ { "participantName": "...", "finishTime": "01:45:30", "overallPosition": 12 } ]` | Public |
| **POST** | `/api/results` | `{ "enrolmentId": 10, "finishTime": "01:45:30", "overallPosition": 12, "categoryPosition": 3 }` | `{ "resultId": 5, "message": "Result uploaded" }` | Organiser Only |