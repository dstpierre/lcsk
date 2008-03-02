using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace LiveChat.Entities
{
    [DataContract]
    public class MessageEntity
    {
        [DataMember]
        public long EntityId { get; set; }
        [DataMember]
        public string ChannelId { get; set; }
        [DataMember]
        public string FromName { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public DateTime SendDate { get; set; }
    }
}
