using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Context;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class DivisionController : Controller
    {
        MyContext myContext;
        public DivisionController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        //GET ALL
        public IActionResult Index()
        {
            return View();
        }

        //GET BY ID
        public IActionResult Details()
        {
            return View();
        }

        //INSERT - GET
        public IActionResult Create()
        {
            return View();
        }

        //INSERT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Division division)
        {
            return View();
        }

        //UPDATE - GET
        public IActionResult Edit(int id)
        {
            return View();
        }

        //UPDATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Division division)
        {
            return View();
        }

        //DELETE - GET
        public IActionResult Delete(int id)
        {
            return View();
        }

        //DELEE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Division division)
        {
            return View();
        }
    }
}
