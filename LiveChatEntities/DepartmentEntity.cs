using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace LiveChat.Entities
{
    [DataContract]
    public class DepartmentEntity
    {
        [DataMember]
        public int EntityId { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
    }
}
