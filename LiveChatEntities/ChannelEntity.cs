using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace LiveChat.Entities
{
    [DataContract]
    public class ChannelEntity
    {
        [DataMember]
        public string EntityId { get; set; }
        [DataMember]
        public int RequestId { get; set; }
        [DataMember]
        public int OperatorId { get; set; }
        [DataMember]
        public DateTime OpenDate { get; set; }
        [DataMember]
        public DateTime? AcceptDate { get; set; }
        [DataMember]
        public DateTime? CloseDate { get; set; }
    }
}
