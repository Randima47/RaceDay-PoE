using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Data;
using RaceDay.Api.DTOs;
using RaceDay.Api.Models;

namespace RaceDay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrolmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EnrolmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/enrolments (Participants only)
        [HttpPost]
        public async Task<IActionResult> EnrolInEvent([FromBody] EnrolmentDto dto)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Participant")
            {
                return StatusCode(403, new { message = "Access denied. Only Participants can enrol in events." });
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return Unauthorized(new { message = "Please log in first." });
            }

            // Check if category exists
            var category = await _context.EventCategories.FindAsync(dto.CategoryID);
            if (category == null)
            {
                return NotFound(new { message = "Selected category not found." });
            }

            var enrolment = new Enrolment
            {
                ParticipantID = userId.Value,
                CategoryID = dto.CategoryID,
                EnrolmentDate = DateTime.Now,
                PaymentStatus = "Pending"
            };

            _context.Enrolments.Add(enrolment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Enrolled successfully!", enrolment });
        }

        // GET: api/enrolments/event/{eventId} (Organisers view enrolments for their event)
        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetEnrolmentsForEvent(int eventId)
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Organiser")
            {
                return StatusCode(403, new { message = "Access denied. Only Organisers can view event enrolments." });
            }

            var enrolments = await _context.Enrolments
                .Include(e => e.Participant)
                .Include(e => e.Category)
                .ThenInclude(c => c.Event)
                .Where(e => e.Category != null && e.Category.EventID == eventId)
                .ToListAsync();

            return Ok(enrolments);
        }
    }
}
