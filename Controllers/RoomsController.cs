using Microsoft.AspNetCore.Mvc;
using Task5.Data;
using Task5.Models;

namespace Task5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllRooms(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        var rooms = DataStore.Rooms.AsEnumerable();

        if (minCapacity.HasValue)
        {
            rooms = rooms.Where(room => room.Capacity >= minCapacity.Value);
        }
        if (hasProjector.HasValue)
        {
            rooms = rooms.Where(r => r.HasProjector == hasProjector.Value);
        }

        if (activeOnly.HasValue && activeOnly.Value)
        {
            rooms = rooms.Where(r => r.IsActive);
        }
        return Ok(DataStore.Rooms);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetRoomById(int id)
    {
        var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
        {
            return NotFound();
        }

        return Ok(room);
    }
    
    [HttpGet("building/{buildingCode}")]
        public IActionResult GetRoomsByBuilding(string buildingCode)
        {
            var rooms = DataStore.Rooms
                .Where(r => r.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(rooms);
        }

        [HttpPost]
        public IActionResult CreateRoom([FromBody] Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            room.Id = DataStore.Rooms.Any() ? DataStore.Rooms.Max(r => r.Id) + 1 : 1;
            DataStore.Rooms.Add(room);

            return CreatedAtAction(nameof(GetRoomById), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, [FromBody] Room updatedRoom)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingRoom = DataStore.Rooms.FirstOrDefault(r => r.Id == id);

            if (existingRoom == null)
            {
                return NotFound();
            }

            existingRoom.Name = updatedRoom.Name;
            existingRoom.BuildingCode = updatedRoom.BuildingCode;
            existingRoom.Floor = updatedRoom.Floor;
            existingRoom.Capacity = updatedRoom.Capacity;
            existingRoom.HasProjector = updatedRoom.HasProjector;
            existingRoom.IsActive = updatedRoom.IsActive;

            return Ok(existingRoom);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);

            if (room == null)
            {
                return NotFound();
            }

            var hasRelatedReservations = DataStore.Reservations.Any(r => r.RoomId == id);

            if (hasRelatedReservations)
            {
                return Conflict("Cannot delete room because related reservations exist.");
            }

            DataStore.Rooms.Remove(room);

            return NoContent();
        }
}