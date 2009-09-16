using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LiveChat.Entities;

namespace LiveChat.WCF
{
    // NOTE: If you change the interface name "IWebRequest" here, you must also update the reference to "IWebRequest" in Web.config.
    [ServiceContract]
    public interface IWebRequest
    {
        [OperationContract]
        void LogRequest(PageRequestEntity page);
        [OperationContract]
        List<PageRequestEntity> GetRequests(DateTime since);
    }
}
