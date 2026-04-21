using Task5.Models;

namespace Task5.Data;

public static class DataStore
{
    public static List<Room> Rooms = new List<Room>()
    {
        new Room
        {
            Id = 1,
            Name = "Lab 101",
            BuildingCode = "A",
            Floor = 1,
            Capacity = 20,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 2,
            Name = "Lab 204",
            BuildingCode = "B",
            Floor = 2,
            Capacity = 24,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 3,
            Name = "Room 305",
            BuildingCode = "A",
            Floor = 3,
            Capacity = 12,
            HasProjector = false,
            IsActive = true
        },
        new Room
        {
            Id = 4,
            Name = "Seminar 12",
            BuildingCode = "C",
            Floor = 1,
            Capacity = 40,
            HasProjector = true,
            IsActive = false
        },
        new Room
        {
            Id = 5,
            Name = "Consulting 7",
            BuildingCode = "B",
            Floor = 0,
            Capacity = 8,
            HasProjector = false,
            IsActive = true
        } 
    };

    public static List<Reservation> Reservations = new List<Reservation>()
    {
        new Reservation
        {
            Id = 1,
            RoomId = 2,
            OrganizerName = "Anna Kowalska",
            Topic = "HTTP and REST Workshop",
            Date = new DateOnly(2026, 5, 10),
            StartTime = new TimeOnly(10, 0),
            EndTime = new TimeOnly(12, 30),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 2,
            RoomId = 1,
            OrganizerName = "Jan Nowak",
            Topic = "C# Basics",
            Date = new DateOnly(2026, 5, 11),
            StartTime = new TimeOnly(9, 0),
            EndTime = new TimeOnly(10, 30),
            Status = "planned"
        },
        new Reservation
        {
            Id = 3,
            RoomId = 3,
            OrganizerName = "Maria Zielinska",
            Topic = "Algorithms Consultation",
            Date = new DateOnly(2026, 5, 11),
            StartTime = new TimeOnly(13, 0),
            EndTime = new TimeOnly(14, 0),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 4,
            RoomId = 5,
            OrganizerName = "Piotr Adamski",
            Topic = "Career Mentoring",
            Date = new DateOnly(2026, 5, 12),
            StartTime = new TimeOnly(15, 0),
            EndTime = new TimeOnly(16, 0),
            Status = "cancelled"
        }
    };
}