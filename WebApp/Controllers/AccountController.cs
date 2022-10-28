using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Context;
using WebApp.Models;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        MyContext myContext;
        ResponseLogin responseLogin = new ResponseLogin();
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
                .SingleOrDefault(x => x.Employee.Email.Equals(email) && x.Password.Equals(password));

            ResponseLogin responseLogin = new ResponseLogin()
            {
                FullName = data.Employee.FullName,
                Email = data.Employee.Email,
                Role = data.Role.Name
            };

            if (data != null) {
                return RedirectToAction("Index", "Home", new {FullName = "Test"});
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
            Employee employee = new Employee()
            {
                FullName = fullName,
                Email = email,
                BirthDate = birthDate,
            };

            myContext.Employees.Add(employee);
            var result = myContext.SaveChanges();
            if(result > 0)
            {
                var id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(email)).Id;
                User user = new User()
                {
                    Id = id,
                    Password = password,
                    RoleId = 1
                };
                myContext.Users.Add(user);
                var resultUser = myContext.SaveChanges();
                if (resultUser > 0)
                    return RedirectToAction("Login", "Account");
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
            ResponseLogin responseLogin = new ResponseLogin();

            var id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(responseLogin.Email)).Id;
            var data = myContext.Users.Find(id);

            if (data != null && (password == passwordConfirm))
            {
                data.Password = password;
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
                return View(users);
            }
            return View();
        }
    }
}
