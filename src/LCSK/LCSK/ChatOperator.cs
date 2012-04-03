using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCSK
{
    public class ChatOperator
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public bool IsOnline { get; set; }
        public DateTime Ping { get; set; }
    }
}