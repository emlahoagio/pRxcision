using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private const string Username = "Admin";
        private const string Password = "123qwe";

        public IActionResult Index()
        {
            return View();
        }

        //POST
        [HttpPost]
        public IActionResult Login(Account account)
        {
            if(account.Username==null || account.Password == null)
            {
                return NotFound();
            }
            if(!account.Username.Equals(Username) || !account.Password.Equals(Password))
            {
                ViewData["loginFail"] = "Username or Password is invalid!";
                return View("Index");
            }
            if(account.Username.Equals(Username) && account.Password.Equals(Password))
            {
                TempData["loginSuccess"] = "Login Successfully!";
                return RedirectToAction("Index", "Data");
            }
            return View("Index");
        }
    }
}