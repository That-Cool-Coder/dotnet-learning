using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Accounts.Models;

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
    }
}
