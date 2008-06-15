using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChat.Entities
{
    public class ChatInvitationEntity
    {
        public int EntityId { get; set; }
        public int OperatorId { get; set; }
        public string VisitorIp { get; set; }
        public string Message { get; set; }
        public bool WasAccept { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
