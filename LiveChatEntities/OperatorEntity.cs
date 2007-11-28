using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChat.Entities
{
    public class OperatorEntity
    {
        public int EntityId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsOnline { get; set; }
        public bool IsAdmin { get; set; }
    }
}
