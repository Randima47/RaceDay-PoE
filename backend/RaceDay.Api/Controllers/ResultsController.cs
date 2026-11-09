using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Data;
using RaceDay.Api.DTOs;
using RaceDay.Api.Models;

namespace RaceDay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/results (Organisers only)
        [HttpPost]
        public async Task<IActionResult> CaptureResult([FromBody] ResultDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Organiser")
            {
                return StatusCode(403, new { message = "Access denied. Only Organisers can capture results." });
            }

            // Verify that the specified enrolment actually exists
            var enrolmentExists = await _context.Enrolments.AnyAsync(e => e.EnrolmentID == dto.EnrolmentID);
            if (!enrolmentExists)
            {
                return NotFound(new { message = "Specified enrolment record not found." });
            }

            // Check if a result has already been logged for this enrolment
            var existingResult = await _context.Results.AnyAsync(r => r.EnrolmentID == dto.EnrolmentID);
            if (existingResult)
            {
                return BadRequest(new { message = "A result has already been captured for this enrolment." });
            }

            var result = new Result
            {
                EnrolmentID = dto.EnrolmentID,
                FinishTime = dto.FinishTime,
                OverallPosition = dto.OverallPosition,
                CategoryPosition = dto.CategoryPosition
            };

            _context.Results.Add(result);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Result captured successfully.", resultId = result.ResultID });
        }

        // GET: api/results/my-results (Participants view their own results)
        [HttpGet("my-results")]
        public async Task<IActionResult> GetMyResults()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Participant")
            {
                return StatusCode(403, new { message = "Access denied. Only Participants can view their results." });
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return Unauthorized(new { message = "Please log in first." });
            }

            var results = await _context.Results
                .Include(r => r.Enrolment)
                .ThenInclude(e => e.Category)
                .ThenInclude(c => c.Event)
                .Where(r => r.Enrolment != null && r.Enrolment.ParticipantID == userId.Value)
                .Select(r => new
                {
                    r.ResultID,
                    EventTitle = r.Enrolment != null && r.Enrolment.Category != null && r.Enrolment.Category.Event != null
                        ? r.Enrolment.Category.Event.Title
                        : "Unknown Event",
                    EventDate = r.Enrolment != null && r.Enrolment.Category != null && r.Enrolment.Category.Event != null
                        ? r.Enrolment.Category.Event.EventDate
                        : (DateTime?)null,
                    CategoryName = r.Enrolment != null && r.Enrolment.Category != null
                        ? r.Enrolment.Category.CategoryName
                        : "N/A",
                    r.FinishTime,
                    r.OverallPosition,
                    r.CategoryPosition
                })
                .ToListAsync();

            return Ok(results);
        }
    }
}