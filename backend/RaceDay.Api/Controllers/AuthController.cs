using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Data;
using RaceDay.Api.Models;
using RaceDay.Api.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace RaceDay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            // 1. Check if email already exists
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            {
                return BadRequest(new { message = "Email is already registered." });
            }

            // 2. Find the role (Organiser or Participant)
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == dto.RoleName);
            if (role == null)
            {
                return BadRequest(new { message = "Invalid role specified. Choose 'Organiser' or 'Participant'." });
            }

            // 3. Hash the password securely (basic SHA256)
            string passwordHash = HashPassword(dto.Password);

            // 4. Create the new user entity
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = passwordHash,
                RoleId = role.RoleId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registration successful!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            // 1. Find user by email
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || user.PasswordHash != HashPassword(dto.Password))
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            // 2. Save user session data
            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserRole", user.Role.RoleName);
            HttpContext.Session.SetInt32("UserId", user.UserId);

            return Ok(new { message = $"Welcome back, {user.Username}!", role = user.Role.RoleName });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok(new { message = "Logged out successfully." });
        }

        // Helper method for hashing passwords
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
