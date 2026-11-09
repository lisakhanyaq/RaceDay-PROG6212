using Microsoft.AspNetCore.Mvc;

namespace RaceDay.Mvc.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
