using Microsoft.AspNetCore.Mvc;

namespace RaceDay.Mvc.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}