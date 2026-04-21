using Microsoft.AspNetCore.Mvc;
using WebApplicationTrainingCenterApi.Data;
using WebApplicationTrainingCenterApi.Models;

namespace WebApplicationTrainingCenterApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> GetAll(
        [FromQuery] DateOnly? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        var query = AppData.Reservations.AsEnumerable();

        if (date.HasValue)
            query = query.Where(r => r.Date == date.Value);

        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(r => r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

        if (roomId.HasValue)
            query = query.Where(r => r.RoomId == roomId.Value);

        return Ok(query);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Reservation> GetById(int id)
    {
        var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation is null)
            return NotFound();

        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult<Reservation> Create([FromBody] Reservation reservation)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);

        if (room is null)
        {
            return BadRequest(new
            {
                message = "Cannot create reservation for a room that does not exist."
            });
        }

        if (!room.IsActive)
        {
            return BadRequest(new
            {
                message = "Cannot create reservation for an inactive room."
            });
        }

        var hasConflict = AppData.Reservations.Any(r =>
            r.RoomId == reservation.RoomId &&
            r.Date == reservation.Date &&
            r.Status.ToLower() != "cancelled" &&
            reservation.Status.ToLower() != "cancelled" &&
            reservation.StartTime < r.EndTime &&
            reservation.EndTime > r.StartTime);

        if (hasConflict)
        {
            return Conflict(new
            {
                message = "Reservation conflicts with an existing reservation for the same room."
            });
        }

        var newId = AppData.Reservations.Any() ? AppData.Reservations.Max(r => r.Id) + 1 : 1;
        reservation.Id = newId;

        AppData.Reservations.Add(reservation);

        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Reservation> Update(int id, [FromBody] Reservation updatedReservation)
    {
        var existingReservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        if (existingReservation is null)
            return NotFound();

        var room = AppData.Rooms.FirstOrDefault(r => r.Id == updatedReservation.RoomId);

        if (room is null)
        {
            return BadRequest(new
            {
                message = "Cannot assign reservation to a room that does not exist."
            });
        }

        if (!room.IsActive)
        {
            return BadRequest(new
            {
                message = "Cannot assign reservation to an inactive room."
            });
        }

        var hasConflict = AppData.Reservations.Any(r =>
            r.Id != id &&
            r.RoomId == updatedReservation.RoomId &&
            r.Date == updatedReservation.Date &&
            r.Status.ToLower() != "cancelled" &&
            updatedReservation.Status.ToLower() != "cancelled" &&
            updatedReservation.StartTime < r.EndTime &&
            updatedReservation.EndTime > r.StartTime);

        if (hasConflict)
        {
            return Conflict(new
            {
                message = "Updated reservation conflicts with an existing reservation for the same room."
            });
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

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation is null)
            return NotFound();

        AppData.Reservations.Remove(reservation);
        return NoContent();
    }
}