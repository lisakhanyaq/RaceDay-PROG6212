using Microsoft.AspNetCore.Mvc;

namespace RaceDay.Mvc.Controllers
{
    public class ResultsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}