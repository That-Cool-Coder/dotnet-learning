using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Accounts.Data;
using Accounts.Models;

namespace Accounts.Controllers
{
    public class TestUsersController : Controller
    {
        private readonly AccountsContext _context;

        public TestUsersController(AccountsContext context)
        {
            _context = context;
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
