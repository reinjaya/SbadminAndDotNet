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
        //[ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string fullName, string email, string password, DateTime birthDate)
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(string password, string passwordConfirm)
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string fullName, string email, DateTime birthDate)
        {
            return View();
        }

        public IActionResult NewPassword(string Id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewPassword(User users, string newPass)
        {          
            return View();
        }
    }
}
