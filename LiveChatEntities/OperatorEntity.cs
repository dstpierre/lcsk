using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace LiveChat.Entities
{
    [DataContract]
    public class OperatorEntity
    {
        [DataMember]
        public int EntityId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public bool IsOnline { get; set; }
        [DataMember]
        public bool IsAdmin { get; set; }
    }
}
