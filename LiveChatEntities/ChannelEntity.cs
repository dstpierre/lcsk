using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChat.Entities
{
    public class ChannelEntity
    {
        public string EntityId { get; set; }
        public int RequestId { get; set; }
        public int OperatorId { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? AcceptDate { get; set; }
        public DateTime? CloseDate { get; set; }
    }
}
