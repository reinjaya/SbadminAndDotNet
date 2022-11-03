using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebApp.Context;
using WebApp.Handlers;
using WebApp.Models;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        MyContext myContext;
        public AccountController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            
            var data = myContext.Users
                .Include(x => x.Employee)
                .Include(x => x.Role)
                .SingleOrDefault(x => x.Employee.Email.Equals(email));

            bool pass = Hashing.ValidatePassword(password, data.Password);

            //ResponseLogin responseLogin = new ResponseLogin()
            //{
            //    FullName = data.Employee.FullName,
            //    Email = data.Employee.Email,
            //    Role = data.Role.Name
            //};

            var id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(email)).Id;
            TempData["IdUser"] = id;

            if (pass) {
                HttpContext.Session.SetInt32("Id", data.Id);
                HttpContext.Session.SetString("FullName", data.Employee.FullName);
                HttpContext.Session.SetString("Email", data.Employee.Email);
                HttpContext.Session.SetString("Role", data.Role.Name);
                return RedirectToAction("Index", "Home", new {Id = id});
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string fullName, string email, string password, DateTime birthDate)
        {
            //var EmailId = myContext.Employees.SingleOrDefault(x => x.Email.Equals(email)).Id;
            var EmailId = myContext.Employees.Where(e => e.Email == email).FirstOrDefault();

            if (EmailId == null)
            {
                Employee employee = new Employee()
                {
                    FullName = fullName,
                    Email = email,
                    BirthDate = birthDate,
                };

                myContext.Employees.Add(employee);
                var result = myContext.SaveChanges();
                if (result > 0)
                {
                    var id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(email)).Id;
                    User user = new User()
                    {
                        Id = id,
                        Password = Hashing.HashPassword(password),
                        RoleId = 1
                    };
                    myContext.Users.Add(user);
                    var resultUser = myContext.SaveChanges();
                    if (resultUser > 0)
                        return RedirectToAction("Login", "Account");
                }
            }
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(string password, string passwordConfirm)
        {
            int IdUser = Convert.ToInt32(HttpContext.Session.GetString("Id"));

            if (IdUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var data = myContext.Users.Find(IdUser);
            if (data != null && (password == passwordConfirm))
            {
                data.Password = Hashing.HashPassword(password);
                myContext.Entry(data).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                if (result > 0)
                    return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string fullName, string email, DateTime birthDate)
        {
            var id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(email)).Id;
            var data = myContext.Employees.Find(id);
            if((data.BirthDate == birthDate) && (data.FullName == fullName))
            {
                var users = myContext.Users.Find(id);
                return RedirectToAction("NewPassword", "Account", new { Id = id });
            }
            return View();
        }

        public IActionResult NewPassword(string Id)
        {
            var UserID = Convert.ToInt32(Id);
            var users = myContext.Users.Find(UserID);
            return View(users);
        }

        [HttpPost]
        public IActionResult NewPassword(User users, string newPass)
        {
            users.Password = Hashing.HashPassword(newPass);
            myContext.Entry(users).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                return RedirectToAction("Login", "Account");
            }               
            return View();
        }
    }
}
