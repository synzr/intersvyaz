using System;

namespace Intersvyaz.Core.Models
{
    public class SessionData
    {
        public string Username { get; set; }

        public string Token { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}
