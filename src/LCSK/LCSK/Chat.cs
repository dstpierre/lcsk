using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCSK
{
    public class Chat
    {
        public Guid Id { get; set; }
        public Guid OperatorId { get; set; }
        public string VisitorIp { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Accepted { get; set; }
        public DateTime? Closed { get; set; }
        public Guid? VisitorId { get; set; }

        public string OperatorName { get; set; }
        public long LastMessageId { get; set; }
    }
}