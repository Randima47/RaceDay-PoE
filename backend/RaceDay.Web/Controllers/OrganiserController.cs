using Microsoft.AspNetCore.Mvc;
using RaceDay.Web.Services;
using RaceDay.Web.ViewModels;

namespace RaceDay.Web.Controllers
{
    public class OrganiserController : Controller
    {
        private readonly ApiService _apiService;

        public OrganiserController(ApiService apiService)
        {
            _apiService = apiService;
        }

        private bool IsOrganiser() => HttpContext.Session.GetString("UserRole") == "Organiser";

        // GET: /Organiser/Dashboard
        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            if (!IsOrganiser()) return RedirectToAction("Login", "Auth");

            var events = await _apiService.GetAllEventsAsync();
            return View(events);
        }

        // GET: /Organiser/CreateEvent
        [HttpGet]
        public IActionResult CreateEvent()
        {
            if (!IsOrganiser()) return RedirectToAction("Login", "Auth");
            return View(new CreateEventViewModel());
        }

        // POST: /Organiser/CreateEvent
        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventViewModel model)
        {
            if (!IsOrganiser()) return RedirectToAction("Login", "Auth");
            if (!ModelState.IsValid) return View(model);

            var success = await _apiService.CreateEventAsync(model);
            if (success)
            {
                TempData["SuccessMessage"] = "Event created successfully!";
                return RedirectToAction("Dashboard");
            }

            ModelState.AddModelError(string.Empty, "Failed to create event via API.");
            return View(model);
        }

        // GET: /Organiser/AddCategory?eventId=5
        [HttpGet]
        public IActionResult AddCategory(int eventId)
        {
            if (!IsOrganiser()) return RedirectToAction("Login", "Auth");
            return View(new CategoryViewModel { EventId = eventId });
        }

        // POST: /Organiser/AddCategory
        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryViewModel model)
        {
            if (!IsOrganiser()) return RedirectToAction("Login", "Auth");
            if (!ModelState.IsValid) return View(model);

            var success = await _apiService.CreateCategoryAsync(model);
            if (success)
            {
                TempData["SuccessMessage"] = "Category added successfully!";
                return RedirectToAction("Dashboard");
            }

            ModelState.AddModelError(string.Empty, "Failed to add category.");
            return View(model);
        }

        // GET: /Organiser/ViewEnrolments/5
        [HttpGet]
        public async Task<IActionResult> ViewEnrolments(int id)
        {
            if (!IsOrganiser()) return RedirectToAction("Login", "Auth");

            var enrolments = await _apiService.GetEnrolmentsForEventAsync(id);
            ViewBag.EventId = id;
            return View(enrolments);
        }

        // GET: /Organiser/CaptureResult?enrolmentId=10
        [HttpGet]
        public IActionResult CaptureResult(int enrolmentId)
        {
            if (!IsOrganiser()) return RedirectToAction("Login", "Auth");
            return View(new CaptureResultViewModel { EnrolmentID = enrolmentId });
        }

        // POST: /Organiser/CaptureResult
        [HttpPost]
        public async Task<IActionResult> CaptureResult(CaptureResultViewModel model)
        {
            if (!IsOrganiser()) return RedirectToAction("Login", "Auth");
            if (!ModelState.IsValid) return View(model);

            var success = await _apiService.CaptureResultAsync(model);
            if (success)
            {
                TempData["SuccessMessage"] = "Result captured successfully!";
                return RedirectToAction("Dashboard");
            }

            ModelState.AddModelError(string.Empty, "Failed to capture result.");
            return View(model);
        }
    }
}