using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accounts.Models
{
    public class SessionId
    {
        public const string CookieName = "AccountsSessionId";

        public int Id { get; set; }
        public float ExprireTimestamp { get; set; }
        public int UserId { get; set; }
        public string Value { get; set; }
    }
}
