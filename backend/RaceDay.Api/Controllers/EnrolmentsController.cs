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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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

            // Check if target category exists
            var category = await _context.EventCategories.FindAsync(dto.CategoryID);
            if (category == null)
            {
                return NotFound(new { message = "Selected category not found." });
            }

            // Check if participant is already enrolled in this category
            var alreadyEnrolled = await _context.Enrolments
                .AnyAsync(e => e.ParticipantID == userId.Value && e.CategoryID == dto.CategoryID);

            if (alreadyEnrolled)
            {
                return BadRequest(new { message = "You are already enrolled in this event category." });
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

            return Ok(new { message = "Enrolled successfully!", enrolmentId = enrolment.EnrolmentID });
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
                .Where(e => e.Category != null && e.Category.EventID == eventId)
                .Select(e => new
                {
                    e.EnrolmentID,
                    ParticipantName = e.Participant != null ? e.Participant.FullName : "N/A",
                    ParticipantEmail = e.Participant != null ? e.Participant.Email : "N/A",
                    CategoryName = e.Category != null ? e.Category.CategoryName : "N/A",
                    e.EnrolmentDate,
                    e.PaymentStatus
                })
                .ToListAsync();

            return Ok(enrolments);
        }
    }
}