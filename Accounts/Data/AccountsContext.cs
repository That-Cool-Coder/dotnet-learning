using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Data
{
    public class AccountsContext : DbContext
    {
        public AccountsContext (DbContextOptions<AccountsContext> options)
            : base(options)
        {
        }

        public DbSet<Accounts.Models.User> User { get; set; }
        public DbSet<Accounts.Models.SessionId> SessionId { get; set; }
    }
}
