using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Data;
using RaceDay.Api.DTOs;
using RaceDay.Api.Models;

namespace RaceDay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/events (Accessible to both Organisers and Participants)
        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _context.Events.ToListAsync();
            return Ok(events);
        }

        // GET: api/events/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var raceEvent = await _context.Events.FindAsync(id);
            if (raceEvent == null)
            {
                return NotFound(new { message = "Event not found." });
            }
            return Ok(raceEvent);
        }

        // POST: api/events (Organisers only)
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventDto dto)
        {
            // Simple Session Check for Organiser Role
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Organiser")
            {
                return StatusCode(403, new { message = "Access denied. Only Organisers can create events." });
            }

            var newEvent = new Event
            {
                Name = dto.Name,
                Description = dto.Description,
                Date = dto.Date,
                Location = dto.Location,
                DistanceKm = dto.DistanceKm,
                EventType = dto.EventType
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEventById), new { id = newEvent.EventId }, newEvent);
        }

        // PUT: api/events/{id} (Organisers only)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventDto dto)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Organiser")
            {
                return StatusCode(403, new { message = "Access denied. Only Organisers can update events." });
            }

            var raceEvent = await _context.Events.FindAsync(id);
            if (raceEvent == null)
            {
                return NotFound(new { message = "Event not found." });
            }

            raceEvent.Name = dto.Name;
            raceEvent.Description = dto.Description;
            raceEvent.Date = dto.Date;
            raceEvent.Location = dto.Location;
            raceEvent.DistanceKm = dto.DistanceKm;
            raceEvent.EventType = dto.EventType;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Event updated successfully.", raceEvent });
        }

        // DELETE: api/events/{id} (Organisers only)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Organiser")
            {
                return StatusCode(403, new { message = "Access denied. Only Organisers can delete events." });
            }

            var raceEvent = await _context.Events.FindAsync(id);
            if (raceEvent == null)
            {
                return NotFound(new { message = "Event not found." });
            }

            _context.Events.Remove(raceEvent);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Event deleted successfully." });
        }
    }
}
