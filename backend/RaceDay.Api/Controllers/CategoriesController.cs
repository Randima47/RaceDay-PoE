using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Data;
using RaceDay.Api.DTOs;
using RaceDay.Api.Models;

namespace RaceDay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/categories/event/{eventId}
        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetCategoriesByEvent(int eventId)
        {
            var categories = await _context.EventCategories
                .Where(c => c.EventID == eventId)
                .ToListAsync();

            if (categories == null || !categories.Any())
            {
                return NotFound(new { message = "No categories found for this event." });
            }

            return Ok(categories);
        }

        // POST: api/categories (Organisers only)
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userRole = HttpContext.Session.GetString("UserRole");
            if (userRole != "Organiser")
            {
                return StatusCode(403, new { message = "Access denied. Only Organisers can add categories." });
            }

            // Verify that the target event actually exists before adding a category
            var eventExists = await _context.Events.AnyAsync(e => e.EventID == dto.EventID);
            if (!eventExists)
            {
                return BadRequest(new { message = "Specified Event ID does not exist." });
            }

            var category = new EventCategory
            {
                EventID = dto.EventID,
                CategoryName = dto.CategoryName,
                DistanceKM = dto.DistanceKM,
                EntryFee = dto.EntryFee
            };

            _context.EventCategories.Add(category);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Category created successfully.", category });
        }
    }
}