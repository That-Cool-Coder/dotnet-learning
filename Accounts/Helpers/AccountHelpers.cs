using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Accounts.Models;
using Accounts.Data;
using Isopoh.Cryptography.Argon2;

namespace Accounts.Helpers
{
    public class AccountHelpers
    {
        public static bool IsLoggedIn(IRequestCookieCollection cookies)
        {
            string sessionIdValue;
            bool sessionIdExists = cookies.TryGetValue(SessionId.CookieName, out sessionIdValue);
            if (!sessionIdExists) return false;
            else if (false /* session id is valid */ ) ;
            else return true;
        }

        public static bool UserExists(AccountsContext context, string username)
        {
            return context.User.Any(e => e.Name == username);
        }

        public async static void CreateUser(AccountsContext context, string username, string password)
        {
            User user = new User();
            user.Name = username;
            user.PasswordHash = Argon2.Hash(password);
            context.Add(user);
            await context.SaveChangesAsync();
        }
    }
}
