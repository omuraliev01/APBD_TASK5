using Microsoft.AspNetCore.Mvc;
using Task5.Data;
using Task5.Models;

namespace Task5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllReservations(
            [FromQuery] DateOnly? date,
            [FromQuery] string? status,
            [FromQuery] int? roomId)
        {
            var reservations = DataStore.Reservations.AsEnumerable();

            if (date.HasValue)
            {
                reservations = reservations.Where(r => r.Date == date.Value);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                reservations = reservations.Where(r =>
                    r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
            }

            if (roomId.HasValue)
            {
                reservations = reservations.Where(r => r.RoomId == roomId.Value);
            }

            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public IActionResult GetReservationById(int id)
        {
            var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        [HttpPost]
        public IActionResult CreateReservation([FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (reservation.EndTime <= reservation.StartTime)
            {
                return BadRequest("EndTime must be later than StartTime.");
            }

            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);

            if (room == null)
            {
                return BadRequest("Room does not exist.");
            }

            if (!room.IsActive)
            {
                return BadRequest("Cannot create reservation for an inactive room.");
            }

            var overlaps = DataStore.Reservations.Any(r =>
                r.RoomId == reservation.RoomId &&
                r.Date == reservation.Date &&
                reservation.StartTime < r.EndTime &&
                reservation.EndTime > r.StartTime);

            if (overlaps)
            {
                return Conflict("Reservation overlaps with an existing reservation.");
            }

            reservation.Id = DataStore.Reservations.Any()
                ? DataStore.Reservations.Max(r => r.Id) + 1
                : 1;

            DataStore.Reservations.Add(reservation);

            return CreatedAtAction(nameof(GetReservationById), new { id = reservation.Id }, reservation);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReservation(int id, [FromBody] Reservation updatedReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (updatedReservation.EndTime <= updatedReservation.StartTime)
            {
                return BadRequest("EndTime must be later than StartTime.");
            }

            var existingReservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);

            if (existingReservation == null)
            {
                return NotFound();
            }

            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == updatedReservation.RoomId);

            if (room == null)
            {
                return BadRequest("Room does not exist.");
            }

            if (!room.IsActive)
            {
                return BadRequest("Cannot create reservation for an inactive room.");
            }

            var overlaps = DataStore.Reservations.Any(r =>
                r.Id != id &&
                r.RoomId == updatedReservation.RoomId &&
                r.Date == updatedReservation.Date &&
                updatedReservation.StartTime < r.EndTime &&
                updatedReservation.EndTime > r.StartTime);

            if (overlaps)
            {
                return Conflict("Reservation overlaps with an existing reservation.");
            }

            existingReservation.RoomId = updatedReservation.RoomId;
            existingReservation.OrganizerName = updatedReservation.OrganizerName;
            existingReservation.Topic = updatedReservation.Topic;
            existingReservation.Date = updatedReservation.Date;
            existingReservation.StartTime = updatedReservation.StartTime;
            existingReservation.EndTime = updatedReservation.EndTime;
            existingReservation.Status = updatedReservation.Status;

            return Ok(existingReservation);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            var reservation = DataStore.Reservations.FirstOrDefault(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            DataStore.Reservations.Remove(reservation);

            return NoContent();
        }
    }
}