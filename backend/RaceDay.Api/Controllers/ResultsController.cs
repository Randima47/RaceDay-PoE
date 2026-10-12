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
            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Organiser")
            {
                return StatusCode(403, new { message = "Access denied. Only Organisers can capture results." });
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

            return Ok(new { message = "Result captured successfully.", result });
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
                .Where(r => r.Enrolment.ParticipantID == userId.Value)
                .ToListAsync();

            return Ok(results);
        }
    }
}