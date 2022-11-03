using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ErrorPagesController : Controller
    {
        public IActionResult Forbidden()
        {
            return View();
        }

        public IActionResult UnAuthorized()
        {
            return View();
        }
    }
}
