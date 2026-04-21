using Microsoft.AspNetCore.Mvc;
using WebApplicationTrainingCenterApi.Data;
using WebApplicationTrainingCenterApi.Models;

namespace WebApplicationTrainingCenterApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Room>> GetAll(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        var query = AppData.Rooms.AsEnumerable();

        if (minCapacity.HasValue)
            query = query.Where(r => r.Capacity >= minCapacity.Value);

        if (hasProjector.HasValue)
            query = query.Where(r => r.HasProjector == hasProjector.Value);

        if (activeOnly == true)
            query = query.Where(r => r.IsActive);

        return Ok(query);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Room> GetById(int id)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);

        if (room is null)
            return NotFound();

        return Ok(room);
    }

    [HttpGet("building/{buildingCode}")]
    public ActionResult<IEnumerable<Room>> GetByBuilding(string buildingCode)
    {
        var rooms = AppData.Rooms
            .Where(r => r.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(rooms);
    }

    [HttpPost]
    public ActionResult<Room> Create([FromBody] Room room)
    {
        var newId = AppData.Rooms.Any() ? AppData.Rooms.Max(r => r.Id) + 1 : 1;
        room.Id = newId;

        AppData.Rooms.Add(room);

        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Room> Update(int id, [FromBody] Room updatedRoom)
    {
        var existingRoom = AppData.Rooms.FirstOrDefault(r => r.Id == id);

        if (existingRoom is null)
            return NotFound();

        existingRoom.Name = updatedRoom.Name;
        existingRoom.BuildingCode = updatedRoom.BuildingCode;
        existingRoom.Floor = updatedRoom.Floor;
        existingRoom.Capacity = updatedRoom.Capacity;
        existingRoom.HasProjector = updatedRoom.HasProjector;
        existingRoom.IsActive = updatedRoom.IsActive;

        return Ok(existingRoom);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == id);

        if (room is null)
            return NotFound();

        var hasReservations = AppData.Reservations.Any(r => r.RoomId == id);

        if (hasReservations)
        {
            return Conflict(new
            {
                message = "Cannot delete room with related reservations."
            });
        }

        AppData.Rooms.Remove(room);
        return NoContent();
    }
}