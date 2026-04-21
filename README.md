\# APBD Tutorial 5 – ASP.NET Core Web API



This project is an ASP.NET Core Web API built using controllers.  

It was created for Tutorial 5 to practice routing, HTTP methods, model binding from route/query/body, validation, and proper HTTP status codes. \[file:1]



\## Project goal



The application simulates a simple training center management system.  

It allows managing classrooms (`Rooms`) and their reservations (`Reservations`) using in-memory data instead of a database. \[file:1]



\## Technologies



\- C#

\- ASP.NET Core Web API

\- Controllers

\- Data Annotations

\- In-memory static lists

\- Postman for API testing \[file:1]



\## Project structure



\- `Controllers/RoomsController.cs` – endpoints for room management \[file:1]

\- `Controllers/ReservationsController.cs` – endpoints for reservation management \[file:1]

\- `Models/Room.cs` – room model \[file:1]

\- `Models/Reservation.cs` – reservation model \[file:1]

\- `Data/DataStore.cs` – static in-memory sample data \[file:1]



\## Data models



\### Room

A room contains:

\- `Id`

\- `Name`

\- `BuildingCode`

\- `Floor`

\- `Capacity`

\- `HasProjector`

\- `IsActive` \[file:1]



\### Reservation

A reservation contains:

\- `Id`

\- `RoomId`

\- `OrganizerName`

\- `Topic`

\- `Date`

\- `StartTime`

\- `EndTime`

\- `Status` \[file:1]



\## Features



\### Rooms

\- Get all rooms \[file:1]

\- Get room by id \[file:1]

\- Get rooms by building code \[file:1]

\- Filter rooms using query parameters \[file:1]

\- Add new room \[file:1]

\- Update existing room \[file:1]

\- Delete room \[file:1]



\### Reservations

\- Get all reservations \[file:1]

\- Get reservation by id \[file:1]

\- Filter reservations using query parameters \[file:1]

\- Add new reservation \[file:1]

\- Update existing reservation \[file:1]

\- Delete reservation \[file:1]



\## Validation



The API includes validation rules required in the task:

\- `Name`, `BuildingCode`, `OrganizerName`, and `Topic` cannot be empty \[file:1]

\- `Capacity` must be greater than zero \[file:1]

\- `EndTime` must be later than `StartTime` \[file:1]



If input data is invalid, the API returns `400 Bad Request`. \[file:1]



\## Business rules



\- A reservation cannot be added for a room that does not exist \[file:1]

\- A reservation cannot be added for an inactive room \[file:1]

\- Two reservations for the same room cannot overlap on the same day \[file:1]

\- Room deletion can be blocked when related reservations exist \[file:1]



\## HTTP status codes



The API uses:

\- `200 OK` for successful reads and updates \[file:1]

\- `201 Created` for successful creation \[file:1]

\- `204 No Content` for successful deletion \[file:1]

\- `404 Not Found` when a resource does not exist \[file:1]

\- `409 Conflict` when a reservation overlaps with an existing one \[file:1]



\## Sample endpoints



\### Rooms

\- `GET /api/rooms`

\- `GET /api/rooms/{id}`

\- `GET /api/rooms/building/{buildingCode}`

\- `GET /api/rooms?minCapacity=20\&hasProjector=true\&activeOnly=true`

\- `POST /api/rooms`

\- `PUT /api/rooms/{id}`

\- `DELETE /api/rooms/{id}` \[file:1]



\### Reservations

\- `GET /api/reservations`

\- `GET /api/reservations/{id}`

\- `GET /api/reservations?date=2026-05-10\&status=confirmed\&roomId=2`

\- `POST /api/reservations`

\- `PUT /api/reservations/{id}`

\- `DELETE /api/reservations/{id}` \[file:1]



\## Running the project



1\. Open the project in Visual Studio.

2\. Restore NuGet packages if needed.

3\. Run the application.

4\. Test endpoints using Postman or Swagger if available. \[file:1]



\## Notes



\- This project does \*\*not\*\* use Entity Framework Core or SQL database. Data is stored only in memory using static lists. \[file:1]

\- The main purpose of the project is educational: practicing REST API design in ASP.NET Core with controllers. \[file:1]

