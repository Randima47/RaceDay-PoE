using Microsoft.AspNetCore.Mvc;
using RaceDay.Web.Services;

namespace RaceDay.Web.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApiService _apiService;

        public EventsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: /Events
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var events = await _apiService.GetAllEventsAsync();
            return View(events);
        }

        // GET: /Events/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var raceEvent = await _apiService.GetEventByIdAsync(id);
            if (raceEvent == null)
            {
                return NotFound();
            }

            return View(raceEvent);
        }
    }
}
