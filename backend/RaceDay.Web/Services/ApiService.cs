using System.Net.Http.Json;
using RaceDay.Web.ViewModels;

namespace RaceDay.Web.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            // API host address port 
            _httpClient.BaseAddress = new Uri("http://localhost:5197/api/");
        }

        // AUTHENTICATION 

        public async Task<UserSessionViewModel?> LoginAsync(LoginViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/login", model);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<UserSessionViewModel>();
        }

        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            // Maps RegisterViewModel properties (Username, RoleName) to match the API's RegisterDto fields
            var payload = new
            {
                username = model.Username,
                email = model.Email,
                password = model.Password,
                roleName = model.RoleName
            };

            var response = await _httpClient.PostAsJsonAsync("auth/register", payload);
            return response.IsSuccessStatusCode;
        }

        //  EVENTS 

        public async Task<List<EventViewModel>> GetAllEventsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<EventViewModel>>("events") ?? new List<EventViewModel>();
            }
            catch
            {
                return new List<EventViewModel>();
            }
        }

        public async Task<EventViewModel?> GetEventByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<EventViewModel>($"events/{id}");
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> CreateEventAsync(CreateEventViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("events", model);
            return response.IsSuccessStatusCode;
        }

        //  CATEGORIES 

        public async Task<bool> CreateCategoryAsync(CategoryViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("categories", model);
            return response.IsSuccessStatusCode;
        }

        //  ENROLMENTS 

        public async Task<bool> EnrolInCategoryAsync(EnrolmentCreateViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("enrolments", model);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<EventEnrolmentViewModel>> GetEnrolmentsForEventAsync(int eventId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<EventEnrolmentViewModel>>($"enrolments/event/{eventId}") ?? new List<EventEnrolmentViewModel>();
            }
            catch
            {
                return new List<EventEnrolmentViewModel>();
            }
        }

        //  RESULTS 

        public async Task<List<ResultViewModel>> GetMyResultsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<ResultViewModel>>("results/my") ?? new List<ResultViewModel>();
            }
            catch
            {
                return new List<ResultViewModel>();
            }
        }

        public async Task<bool> CaptureResultAsync(CaptureResultViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("results", model);
            return response.IsSuccessStatusCode;
        }
    }
}