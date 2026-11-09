using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;

namespace StudentManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        // Constructor Injection of AppDbContext
        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        // Asynchronous Index action to retrieve and display student records
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync();
            return View(students);
        }
    }
}
