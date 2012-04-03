using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCSK
{
    public class ChatMessage
    {
        public long Id { get; set; }
        public Guid ChatId { get; set; }
        public string FromName { get; set; }
        public string Message { get; set; }
        public DateTime Sent { get; set; }
    }
}