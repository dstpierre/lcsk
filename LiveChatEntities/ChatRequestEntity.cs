using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace LiveChat.Entities
{
    [DataContract]
    public class ChatRequestEntity
    {
        [DataMember]
        public int EntityId { get; set; }
        [DataMember]
        public string VisitorIp { get; set; }
        [DataMember]
        public string VisitorName { get; set; }
        [DataMember]
        public string VisitorEmail { get; set; }
        [DataMember]
        public int DepartmentId { get; set; }
        [DataMember]
        public bool SendCopyOfChat { get; set; }
        [DataMember]
        public DateTime RequestedDate { get; set; }
    }
}
