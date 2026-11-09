using Microsoft.AspNetCore.Mvc;
using RaceDay.Mvc.Models;
using RaceDay.Mvc.Services;
using RaceDay.Mvc.ViewModels;

namespace RaceDay.Mvc.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApiService _apiService;

        public EventsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var events =
                await _apiService.GetAsync<List<EventViewModel>>("events");

            return View(events);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var events =
                await _apiService.GetAsync<List<EventViewModel>>("events");

            var ev = events?
                .FirstOrDefault(e => e.EventId == id);

            if (ev == null)
                return NotFound();

            return View(ev);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var request = new CreateEventRequest
            {
                Name = model.Name,
                Description = model.Description,
                EventDate = model.EventDate,
                Location = model.Location,
                DistanceKm = model.DistanceKm,
                EventType = model.EventType,
                OrganiserId = 1
            };

            var response = await _apiService.PostAsync("events", request);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = $"API Error: {response.StatusCode}";
                return View(model);
            }

            TempData["Success"] = "Event created successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}