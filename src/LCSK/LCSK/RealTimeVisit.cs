using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LCSK
{
    public class RealTimeVisit
    {
        public Guid Id { get; set; }
        public string VisitorIp { get; set; }
        public string PageRequested { get; set; }
        public string Referrer { get; set; }
        public DateTime RequestedOn { get; set; }
        public DateTime Ping { get; set; }
        public Guid InChatId { get; set; }
    }
}