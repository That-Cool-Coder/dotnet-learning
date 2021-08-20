using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using Isopoh.Cryptography.Argon2;

using Accounts.Models;
using Accounts.Data;

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

            bool loggedIn = IsLoggedIn(Request.Cookies);
            return View(loggedIn);
        }

        public IActionResult App()
        {
            bool loggedIn = IsLoggedIn(Request.Cookies);
            if (loggedIn) return View(loggedIn);
            else return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            bool loggedIn = IsLoggedIn(Request.Cookies);
            return View(loggedIn);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (!UserExists(username)) return BadRequest();

            User user = GetUsers().Where(s => s.Name.Equals(username)).ToList()[0];

            if (!Argon2.Verify(user.PasswordHash, password)) return BadRequest();

            SessionId sessionId = new SessionId();
            sessionId.Value = Guid.NewGuid().ToString();
            sessionId.UserId = user.Id;
            sessionId.ExprireTimestamp = 1000;
            _context.Add(sessionId);
            await _context.SaveChangesAsync();

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(5);
            Response.Cookies.Append(SessionId.CookieName, sessionId.Value, option);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateAccount()
        {
            bool loggedIn = IsLoggedIn(Request.Cookies);
            return View(loggedIn);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(string username, string password)
        {
            User user = new User();
            user.Name = username;
            user.PasswordHash = Argon2.Hash(password);
            _context.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            // todo: write this
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool IsLoggedIn(IRequestCookieCollection cookies)
        {
            string sessionIdValue;
            bool sessionIdPresent = cookies.TryGetValue(SessionId.CookieName, out sessionIdValue);
            if (!sessionIdPresent) return false;

            List<SessionId> sessionIdsMatching = GetSessionIds().Where(s => s.Value.Equals(sessionIdValue)).ToList();
            if (sessionIdsMatching.Count() == 0) return false;
            else return true;
        }

        private IQueryable<SessionId> GetSessionIds()
        {
            return from m in _context.SessionId
                   select m;
        }
        
        private IQueryable<User> GetUsers()
        {
            return from m in _context.User
                   select m;
        }

        private bool UserExists(string username)
        {
            return _context.User.Any(e => e.Name == username);
        }
    }
}
