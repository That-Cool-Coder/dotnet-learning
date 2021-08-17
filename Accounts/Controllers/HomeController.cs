using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Accounts.Models;
using Accounts.Data;
using Accounts.Helpers;

namespace Accounts.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AccountsContext _context;

        public HomeController(ILogger<HomeController> logger, AccountsContext context)
        {
            _logger = logger;
            _context = context;
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

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (AccountHelpers.UserExists(_context, username))
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddMinutes(5);
                Response.Cookies.Append(SessionId.CookieName, "Haha cookie", option);
                return RedirectToAction(nameof(Index));
            }
            else return BadRequest();
        }

        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(string username, string password)
        {
            AccountHelpers.CreateUser(_context, username, password);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
