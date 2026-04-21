using WebApplicationTrainingCenterApi.Models;

namespace WebApplicationTrainingCenterApi.Data;

public static class AppData
{
    public static List<Room> Rooms { get; } = new()
    {
        new Room
        {
            Id = 1,
            Name = "Room A101",
            BuildingCode = "A",
            Floor = 1,
            Capacity = 20,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 2,
            Name = "Lab B204",
            BuildingCode = "B",
            Floor = 2,
            Capacity = 24,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 3,
            Name = "Room A305",
            BuildingCode = "A",
            Floor = 3,
            Capacity = 12,
            HasProjector = false,
            IsActive = true
        },
        new Room
        {
            Id = 4,
            Name = "Room C110",
            BuildingCode = "C",
            Floor = 1,
            Capacity = 30,
            HasProjector = true,
            IsActive = false
        },
        new Room
        {
            Id = 5,
            Name = "Workshop B010",
            BuildingCode = "B",
            Floor = 0,
            Capacity = 18,
            HasProjector = false,
            IsActive = true
        }
    };

    public static List<Reservation> Reservations { get; } = new()
    {
        new Reservation
        {
            Id = 1,
            RoomId = 1,
            OrganizerName = "Anna Kowalska",
            Topic = "ASP.NET Core Basics",
            Date = new DateOnly(2026, 5, 10),
            StartTime = new TimeOnly(9, 0),
            EndTime = new TimeOnly(11, 0),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 2,
            RoomId = 2,
            OrganizerName = "Jan Nowak",
            Topic = "REST Workshop",
            Date = new DateOnly(2026, 5, 10),
            StartTime = new TimeOnly(10, 0),
            EndTime = new TimeOnly(12, 30),
            Status = "planned"
        },
        new Reservation
        {
            Id = 3,
            RoomId = 3,
            OrganizerName = "Ewa Zielińska",
            Topic = "Consultation Session",
            Date = new DateOnly(2026, 5, 11),
            StartTime = new TimeOnly(8, 30),
            EndTime = new TimeOnly(10, 0),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 4,
            RoomId = 5,
            OrganizerName = "Piotr Wiśniewski",
            Topic = "Testing API in Postman",
            Date = new DateOnly(2026, 5, 12),
            StartTime = new TimeOnly(13, 0),
            EndTime = new TimeOnly(15, 0),
            Status = "cancelled"
        },
        new Reservation
        {
            Id = 5,
            RoomId = 1,
            OrganizerName = "Maria Lewandowska",
            Topic = "HTTP Deep Dive",
            Date = new DateOnly(2026, 5, 13),
            StartTime = new TimeOnly(12, 0),
            EndTime = new TimeOnly(14, 0),
            Status = "planned"
        }
    };
}