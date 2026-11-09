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

        // GET: api/events
        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _context.Events
                .Include(e => e.Organiser)
                .Include(e => e.Categories)
                .Select(e => new
                {
                    e.EventID,
                    e.Title,
                    e.Description,
                    e.EventDate,
                    e.Location,
                    e.BannerUrl,
                    OrganiserName = e.Organiser != null ? e.Organiser.FullName : "Unknown",
                    Categories = e.Categories.Select(c => new
                    {
                        c.CategoryID,
                        c.CategoryName,
                        c.DistanceKM,
                        c.EntryFee
                    })
                })
                .ToListAsync();

            return Ok(events);
        }

        // GET: api/events/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var raceEvent = await _context.Events
                .Include(e => e.Organiser)
                .Include(e => e.Categories)
                .Where(e => e.EventID == id)
                .Select(e => new
                {
                    e.EventID,
                    e.Title,
                    e.Description,
                    e.EventDate,
                    e.Location,
                    e.BannerUrl,
                    OrganiserName = e.Organiser != null ? e.Organiser.FullName : "Unknown",
                    Categories = e.Categories.Select(c => new
                    {
                        c.CategoryID,
                        c.CategoryName,
                        c.DistanceKM,
                        c.EntryFee
                    })
                })
                .FirstOrDefaultAsync();

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Organiser")
            {
                return StatusCode(403, new { message = "Access denied. Only Organisers can create events." });
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return Unauthorized(new { message = "User session not found. Please log in." });
            }

            var newEvent = new Event
            {
                Title = dto.Title,
                Description = dto.Description,
                EventDate = dto.EventDate,
                Location = dto.Location,
                BannerUrl = dto.BannerUrl, // Supports Azure Blob storage image URL string
                OrganiserID = userId.Value
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEventById), new { id = newEvent.EventID }, new { message = "Event created successfully.", newEvent });
        }

        // PUT: api/events/{id} (Organisers only)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

            raceEvent.Title = dto.Title;
            raceEvent.Description = dto.Description;
            raceEvent.EventDate = dto.EventDate;
            raceEvent.Location = dto.Location;
            if (!string.IsNullOrEmpty(dto.BannerUrl))
            {
                raceEvent.BannerUrl = dto.BannerUrl;
            }

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