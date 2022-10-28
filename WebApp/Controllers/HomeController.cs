using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApp.Context;
using WebApp.Models;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        MyContext myContext;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MyContext myContext)
        {
            _logger = logger;
            this.myContext = myContext;
        }


        public IActionResult Index(string id)
        {
            if ( id != null)
            {
                var IdUser = Convert.ToInt32(id);
                var data = myContext.Users
                    .Include(x => x.Employee)
                    .Include(x => x.Role)
                    .SingleOrDefault(x => x.Id.Equals(IdUser));

                ResponseLogin responseLogin = new ResponseLogin()
                {
                    FullName = data.Employee.FullName,
                    Email = data.Employee.Email,
                    Role = data.Role.Name
                };
                return View(responseLogin);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}