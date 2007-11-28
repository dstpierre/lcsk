using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveChat.Entities
{
    public class MessageEntity
    {
        public long EntityId { get; set; }
        public string ChannelId { get; set; }
        public string FromName { get; set; }
        public string Message { get; set; }
        public DateTime SendDate { get; set; }
    }
}
