using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Models;
using Accounts.Helpers;

namespace Accounts.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            bool loggedIn = AccountHelpers.IsLoggedIn(Request.Cookies);
            return View(loggedIn);
        }

        public IActionResult App()
        {
            bool loggedIn = AccountHelpers.IsLoggedIn(Request.Cookies);
            if (loggedIn) return View();
            else return RedirectToAction("Login");
        }

        public IActionResult Login()
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
