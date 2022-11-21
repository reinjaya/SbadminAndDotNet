using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Context;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class DepartementController : Controller
    {
        //GET ALL
        public IActionResult Index()
        {
            return View();
        }

        //GET BY ID
        public IActionResult Details(int id)
        {
            return View();
        }

        //CREATE - GET
        public IActionResult Create()
        {
            return View();
        }

        //CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Departement departement)
        {
            return View();
        }

        //EDIT - GET
        public IActionResult Edit(int id)
        {
            return View();
        }

        //EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Departement departement)
        {
            return View();
        }

        //DELETE - GET
        public IActionResult Delete(int id)
        {
            return View();
        }

        //DELETE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Departement departement)
        {
            return View();
        }
    }
}
