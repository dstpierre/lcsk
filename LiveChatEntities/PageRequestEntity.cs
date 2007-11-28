using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChat.Entities
{
    public class PageRequestEntity
    {
        public int EntityId { get; set; }
        public string VisitorIp { get; set; }
        public string PageRequested { get; set; }
        public string Referrer { get; set; }
        public string UserAgent { get; set; }
        public DateTime RequestedDate { get; set; }
    }
}
