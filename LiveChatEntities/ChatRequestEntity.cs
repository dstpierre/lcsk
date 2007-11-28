using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChat.Entities
{
    public class ChatRequestEntity
    {
        public int EntityId { get; set; }
        public string VisitorIp { get; set; }
        public string VisitorName { get; set; }
        public string VisitorEmail { get; set; }
        public int DepartmentId { get; set; }
        public bool SendCopyOfChat { get; set; }
        public DateTime RequestedDate { get; set; }
    }
}
