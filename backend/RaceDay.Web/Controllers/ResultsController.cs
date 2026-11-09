using Microsoft.AspNetCore.Mvc;
using RaceDay.Web.Services;

namespace RaceDay.Web.Controllers
{
    public class ResultsController : Controller
    {
        private readonly ApiService _apiService;

        public ResultsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: /Results/MyResults
        [HttpGet]
        public async Task<IActionResult> MyResults()
        {
            if (HttpContext.Session.GetString("UserRole") != "Participant")
            {
                return RedirectToAction("Login", "Auth");
            }

            var results = await _apiService.GetMyResultsAsync();
            return View(results);
        }
    }
}