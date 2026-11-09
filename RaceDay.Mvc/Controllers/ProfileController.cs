using Microsoft.AspNetCore.Mvc;

namespace RaceDay.Mvc.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}