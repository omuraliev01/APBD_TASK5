# APBD Tutorial 5 – ASP.NET Core Web API

This project is an ASP.NET Core Web API built using controllers.

It was created for Tutorial 5 to practice routing, HTTP methods, model binding from route/query/body, validation, and proper HTTP status codes.

## Project goal

The application simulates a simple training center management system.

It allows managing classrooms (`Rooms`) and their reservations (`Reservations`) using in-memory data instead of a database.

## Technologies

- C#
- ASP.NET Core Web API
- Controllers
- Data Annotations
- In-memory static lists
- Postman for API testing

## Project structure

- `Controllers/RoomsController.cs` – endpoints for room management
- `Controllers/ReservationsController.cs` – endpoints for reservation management
- `Models/Room.cs` – room model
- `Models/Reservation.cs` – reservation model
- `Data/DataStore.cs` – static in-memory sample data

## Data models

### Room

A room contains:
- `Id`
- `Name`
- `BuildingCode`
- `Floor`
- `Capacity`
- `HasProjector`
- `IsActive`

### Reservation

A reservation contains:
- `Id`
- `RoomId`
- `OrganizerName`
- `Topic`
- `Date`
- `StartTime`
- `EndTime`
- `Status`

## Features

### Rooms
- Get all rooms
- Get room by id
- Get rooms by building code
- Filter rooms using query parameters
- Add new room
- Update existing room
- Delete room

### Reservations
- Get all reservations
- Get reservation by id
- Filter reservations using query parameters
- Add new reservation
- Update existing reservation
- Delete reservation

## Validation

The API includes validation rules:
- `Name`, `BuildingCode`, `OrganizerName`, and `Topic` cannot be empty
- `Capacity` must be greater than zero
- `EndTime` must be later than `StartTime`

If input data is invalid, the API returns `400 Bad Request`.

## Business rules

- A reservation cannot be added for a room that does not exist
- A reservation cannot be added for an inactive room
- Two reservations for the same room cannot overlap on the same day
- Room deletion can be blocked when related reservations exist

## HTTP status codes

The API uses:
- `200 OK` for successful reads and updates
- `201 Created` for successful creation
- `204 No Content` for successful deletion
- `404 Not Found` when a resource does not exist
- `409 Conflict` when a reservation overlaps with an existing one

## Sample endpoints

### Rooms
- `GET /api/rooms`
- `GET /api/rooms/{id}`
- `GET /api/rooms/building/{buildingCode}`
- `GET /api/rooms?minCapacity=20&hasProjector=true&activeOnly=true`
- `POST /api/rooms`
- `PUT /api/rooms/{id}`
- `DELETE /api/rooms/{id}`

### Reservations
- `GET /api/reservations`
- `GET /api/reservations/{id}`
- `GET /api/reservations?date=2026-05-10&status=confirmed&roomId=2`
- `POST /api/reservations`
- `PUT /api/reservations/{id}`
- `DELETE /api/reservations/{id}`

## Running the project

1. Open the project in Visual Studio.
2. Restore NuGet packages if needed.
3. Run the application.
4. Test endpoints using Postman or Swagger if available.

## Notes

- This project does not use Entity Framework Core or SQL database.
- Data is stored only in memory using static lists.
- The main purpose of the project is educational: practicing REST API design in ASP.NET Core with controllers.
