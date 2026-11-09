using Microsoft.AspNetCore.Mvc;
using RaceDay.Mvc.ViewModels;

namespace RaceDay.Mvc.Controllers
{
    public class EnrolmentsController : Controller
    {
        [HttpGet]
        public IActionResult Create(int id)
        {
            var model = new CreateEnrolmentViewModel
            {
                EventId = id
            };

            return View(model);
        }
        [HttpGet]
        public IActionResult MyEvents()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEnrolmentViewModel model)
        {
            TempData["Success"] =
                "Enrolment submitted successfully";

            return RedirectToAction(
                "Index",
                "Events");
        }
    }
}