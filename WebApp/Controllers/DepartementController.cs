using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Context;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class DepartementController : Controller
    {
        MyContext myContext;
        public DepartementController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        //GET ALL
        public IActionResult Index()
        {
            var data = myContext.Departements.ToList();
            return View(data);
        }

        //GET BY ID
        public IActionResult Details(int id)
        {
            var data = myContext.Departements.Find(id);
            return View(data);
        }

        //CREATE - GET
        public IActionResult Create()
        {
            var data = new MyViewModel();
            data.Divisions = myContext.Divisions.Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();

            return View(data);
        }

        //CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Departement departement)
        {
            myContext.Departements.Add(departement);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction("Index", "Departement");
            }
            return View(result);
        }

        //EDIT - GET
        public IActionResult Edit(int id)
        {
            var data = myContext.Departements.Find(id);
            return View(data);
        }

        //EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Departement departement)
        {
            var data = myContext.Departements.Find(id);
            if (data != null)
            {
                data.Name = departement.Name;
                data.DivisionId = departement.DivisionId;
                myContext.Entry(data).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result > 0)
                    return RedirectToAction("Index", "Departement");
            }
            return View();
        }

        //DELETE - GET
        public IActionResult Delete(int id)
        {
            var data = myContext.Departements.Find(id);
            return View(data);
        }

        //DELETE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Departement departement)
        {
            myContext.Departements.Remove(departement);
            var result = myContext.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "Departement");
            return View();
        }
    }
}
