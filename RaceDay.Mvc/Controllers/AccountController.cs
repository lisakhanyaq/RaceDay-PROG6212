using Microsoft.AspNetCore.Mvc;
using RaceDay.Mvc.ViewModels;

namespace RaceDay.Mvc.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            HttpContext.Session.SetString(
                "UserEmail",
                model.Email);

            return RedirectToAction(
                "Index",
                "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(
                nameof(Login));
        }
    }
}