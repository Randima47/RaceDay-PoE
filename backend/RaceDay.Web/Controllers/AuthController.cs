using Microsoft.AspNetCore.Mvc;
using RaceDay.Web.Services;
using RaceDay.Web.ViewModels;

namespace RaceDay.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApiService _apiService;

        public AuthController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: /Auth/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        // POST: /Auth/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var userSession = await _apiService.LoginAsync(model);
            if (userSession != null)
            {
                // Save session parameters for role-based navigation & API actions
                HttpContext.Session.SetString("UserName", userSession.Name ?? string.Empty);
                HttpContext.Session.SetString("UserEmail", userSession.Email ?? string.Empty);
                HttpContext.Session.SetString("UserRole", userSession.Role ?? string.Empty);

                TempData["SuccessMessage"] = $"Welcome back, {userSession.Name}!";

                if (userSession.Role == "Organiser")
                {
                    return RedirectToAction("Dashboard", "Organiser");
                }
                return RedirectToAction("Index", "Events");
            }

            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View(model);
        }

        // GET: /Auth/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        // POST: /Auth/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var success = await _apiService.RegisterAsync(model);
            if (success)
            {
                TempData["SuccessMessage"] = "Registration successful! Please log in.";
                return RedirectToAction("Login");
            }

            ModelState.AddModelError(string.Empty, "Registration failed. Email may already be registered or invalid role.");
            return View(model);
        }

        // GET: /Auth/Logout
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "Logged out successfully.";
            return RedirectToAction("Login");
        }
    }
}